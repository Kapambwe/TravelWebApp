$ErrorActionPreference = "Stop"
$ProgressPreference    = "SilentlyContinue"

# -- Configuration -------------------------------------------------------------
$AppName           = "TravelWebApp"
$CsprojPath        = Join-Path $PSScriptRoot "$AppName.csproj"
$PublishDir        = "C:\Public\$AppName"
$PublicRepoUrl     = "https://github.com/Kapambwe/$AppName.git"
$GitHubPagesBranch = "gh-pages"
$GitHubPagesUrl    = "https://Kapambwe.github.io/$AppName/"

Write-Host ""
Write-Host "========================================" -ForegroundColor Magenta
Write-Host "DEPLOYING: $AppName to GitHub Pages" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta
Write-Host ""

# 1. Build and Publish locally
$tempDir = Join-Path ([System.IO.Path]::GetTempPath()) ("$AppName-$(Get-Random)")
Write-Host "Building and publishing $AppName in Release mode..." -ForegroundColor Yellow

dotnet publish $CsprojPath -c Release -o $tempDir --nologo
if ($LASTEXITCODE -ne 0) {
    Write-Error "dotnet publish failed (exit $LASTEXITCODE)"
    exit 1
}
Write-Host "  [OK] Build complete." -ForegroundColor Green

$wwwrootSrc = Join-Path $tempDir "wwwroot"
if (-not (Test-Path $wwwrootSrc)) {
    $wwwrootSrc = $tempDir
}

# Add .nojekyll to prevent Jekyll from ignoring Blazor files starting with underscore
$noJekyll = Join-Path $wwwrootSrc ".nojekyll"
if (-not (Test-Path $noJekyll)) {
    Out-File -FilePath $noJekyll -InputObject "" -Encoding ascii
    Write-Host "  [OK] .nojekyll file created." -ForegroundColor Green
}

# Add .gitattributes to force binary treatment and prevent git from converting line endings
$gitAttributes = Join-Path $wwwrootSrc ".gitattributes"
if (-not (Test-Path $gitAttributes)) {
    Out-File -FilePath $gitAttributes -InputObject "* binary" -Encoding ascii
    Write-Host "  [OK] .gitattributes file created." -ForegroundColor Green
}

# Fix base href in published index.html for subdirectory hosting
$indexPath = Join-Path $wwwrootSrc "index.html"
if (Test-Path $indexPath) {
    $html = Get-Content $indexPath -Raw
    $baseTag = '<base href="/' + $AppName + '/" />'
    $html = $html -replace '(?s)<!-- Smart base path.*?<\/script>', $baseTag
    Set-Content $indexPath -Value $html -NoNewline -Encoding utf8
    Write-Host "  [OK] Patched index.html base href to '/$AppName/'" -ForegroundColor Green
}

# Refresh local publish deployment directory
Write-Host "Refreshing deployment directory $PublishDir..." -ForegroundColor Yellow
if (Test-Path $PublishDir) {
    Get-ChildItem -Path $PublishDir -Force |
        Where-Object { $_.Name -ne ".git" } |
        Remove-Item -Recurse -Force
} else {
    New-Item -Path $PublishDir -ItemType Directory | Out-Null
}

# Copy files
Copy-Item -Path "$wwwrootSrc\*" -Destination $PublishDir -Recurse -Force
Write-Host "  [OK] Files copied to deploy folder." -ForegroundColor Green

# Clean up temp build folder
Remove-Item -Path $tempDir -Recurse -Force -ErrorAction SilentlyContinue

# Initialize/configure git in the public publish folder
if (-not (Test-Path (Join-Path $PublishDir ".git"))) {
    Write-Host "Initializing git repository in $PublishDir..." -ForegroundColor Yellow
    git -C $PublishDir init -b $GitHubPagesBranch
    git -C $PublishDir remote add origin $PublicRepoUrl
} else {
    git -C $PublishDir remote set-url origin $PublicRepoUrl
}

git -C $PublishDir config core.autocrlf false

# Commit and force push to origin/gh-pages
Write-Host "Committing and force-pushing to $GitHubPagesBranch branch..." -ForegroundColor Yellow
git -C $PublishDir add -A
$timestamp = Get-Date -Format "yyyy-MM-dd HH:mm"
$commitMsg = "deploy: " + $AppName + " - " + $timestamp
git -C $PublishDir commit -m $commitMsg --allow-empty --quiet
git -C $PublishDir push origin $GitHubPagesBranch --force

if ($LASTEXITCODE -ne 0) {
    Write-Error "Git push failed!"
    exit 1
}

Write-Host ""
Write-Host "[OK] Successfully deployed $AppName to GitHub Pages!" -ForegroundColor Green
Write-Host "Live URL: $GitHubPagesUrl" -ForegroundColor Cyan
Write-Host "(Please note it may take ~1 minute for GitHub Pages to update)" -ForegroundColor Gray
Write-Host ""
