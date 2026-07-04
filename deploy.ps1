# Get repository root based on script location (src/WebApps/TravelWebApp/deploy.ps1 -> 3 levels up)
$repoRoot = Resolve-Path (Join-Path $PSScriptRoot "../../..")
$oldDir = Get-Location

# Change directory to repo root to execute dotnet and wrangler commands
Set-Location $repoRoot

$appName = "TravelWebApp"
$projectPath = "src/WebApps/TravelWebApp/TravelWebApp.csproj"
$cloudflareName = "travel-webapp"

# Check for required environment variables
if (-not $env:CLOUDFLARE_API_TOKEN) {
    Write-Error "CLOUDFLARE_API_TOKEN environment variable is not set"
    Set-Location $oldDir
    exit 1
}

if (-not $env:CLOUDFLARE_ACCOUNT_ID) {
    Write-Error "CLOUDFLARE_ACCOUNT_ID environment variable is not set"
    Set-Location $oldDir
    exit 1
}

# Function to create Cloudflare Pages project via API
function New-CloudflareProject {
    param(
        [string]$ProjectName,
        [string]$AccountId,
        [string]$ApiToken
    )
    
    Write-Host "Checking if project '$ProjectName' exists..." -ForegroundColor Yellow
    
    # Check if project already exists
    $headers = @{
        "Authorization" = "Bearer $ApiToken"
        "Content-Type" = "application/json"
    }
    
    $checkUrl = "https://api.cloudflare.com/client/v4/accounts/$AccountId/pages/projects/$ProjectName"
    
    try {
        $response = Invoke-RestMethod -Uri $checkUrl -Headers $headers -Method Get -ErrorAction Stop
        Write-Host "  Project already exists, skipping creation." -ForegroundColor Green
        return $true
    }
    catch {
        if ($_.Exception.Response.StatusCode -eq 404) {
            Write-Host "  Project doesn't exist, creating..." -ForegroundColor Yellow
            
            # Create the project
            $createUrl = "https://api.cloudflare.com/client/v4/accounts/$AccountId/pages/projects"
            $body = @{
                name = $ProjectName
                production_branch = "main"
            } | ConvertTo-Json
            
            try {
                $createResponse = Invoke-RestMethod -Uri $createUrl -Headers $headers -Method Post -Body $body -ErrorAction Stop
                Write-Host "  ✓ Project created successfully!" -ForegroundColor Green
                return $true
            }
            catch {
                Write-Error "Failed to create project: $_"
                return $false
            }
        }
        else {
            Write-Error "Failed to check project: $_"
            return $false
        }
    }
}

Write-Host "`n========================================" -ForegroundColor Magenta
Write-Host "STEP 1: Creating Cloudflare Pages Project" -ForegroundColor Magenta
Write-Host "========================================`n" -ForegroundColor Magenta

$created = New-CloudflareProject -ProjectName $cloudflareName -AccountId $env:CLOUDFLARE_ACCOUNT_ID -ApiToken $env:CLOUDFLARE_API_TOKEN
if (-not $created) {
    Write-Warning "Failed to create project for $appName, deployment may fail"
}

Write-Host "`n========================================" -ForegroundColor Magenta
Write-Host "STEP 2: Building and Deploying $appName" -ForegroundColor Magenta
Write-Host "========================================`n" -ForegroundColor Magenta

try {
    # Check if project file exists
    if (-not (Test-Path $projectPath)) {
        throw "Project file not found at: $projectPath"
    }
    
    # Restore dependencies
    Write-Host "Restoring dependencies..." -ForegroundColor Yellow
    dotnet restore $projectPath
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to restore dependencies"
    }
    
    # Build the project
    Write-Host "Building project..." -ForegroundColor Yellow
    dotnet build $projectPath --configuration Release --no-restore
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to build project"
    }
    
    # Publish the project
    Write-Host "Publishing project..." -ForegroundColor Yellow
    $publishDir = "publish/$appName"
    dotnet publish $projectPath --configuration Release --output $publishDir --no-build
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to publish project"
    }
    
    # Fix base href in published index.html for Cloudflare Pages (uses root path)
    $indexHtml = Join-Path $publishDir "wwwroot/index.html"
    if (Test-Path $indexHtml) {
        $html = Get-Content $indexHtml -Raw
        $html = $html -replace '(?s)<!-- Smart base path.*?<\/script>', '<base href="/" />'
        Set-Content $indexHtml -Value $html -NoNewline -Encoding utf8
        Write-Host "  ✓ Patched index.html base href to '/' for Cloudflare Pages deployment" -ForegroundColor Green
    }
    
    # Deploy to Cloudflare Pages using Wrangler
    Write-Host "Deploying to Cloudflare Pages..." -ForegroundColor Yellow
    $wwwrootPath = Join-Path $publishDir "wwwroot"
    
    if (-not (Test-Path $wwwrootPath)) {
        throw "wwwroot folder not found at: $wwwrootPath"
    }
    
    # Deploy using wrangler pages deploy
    Write-Host "  Project name: $cloudflareName" -ForegroundColor Cyan
    Write-Host "  Deploying from: $wwwrootPath" -ForegroundColor Cyan
    
    wrangler pages deploy $wwwrootPath --project-name=$cloudflareName --branch=main --commit-dirty
        
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to deploy to Cloudflare Pages"
    }
    
    Write-Host "`n✓ Successfully deployed $appName to Cloudflare Pages" -ForegroundColor Green
    
    # Clean up publish directory
    Remove-Item -Path $publishDir -Recurse -Force -ErrorAction SilentlyContinue
    
} catch {
    Write-Error "Failed to deploy $appName: $_"
    Set-Location $oldDir
    exit 1
}

# Restore original working directory
Set-Location $oldDir
