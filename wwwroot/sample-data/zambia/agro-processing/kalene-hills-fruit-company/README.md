# Kalene Hills Fruit CompanyWebApp Sample Data Documentation

This directory contains sample JSON data files for all Mock Services used by the Kalene Hills Fruit CompanyWebApp. These files provide realistic sample data for development, testing, and demonstration purposes.

## Overview

The Kalene Hills Fruit CompanyWebApp uses **51 Mock Services** that provide sample data when the application is running in development mode with `UseMockServices: true` in the configuration.

## Directory Structure

```
wwwroot/sample-data/kalene-hills-fruit-company/
├── README.md (this file)
├── kalene-hills-fruit-company-service.json
├── recurring-transaction-service.json
├── journal-entry-service.json
├── estimate-service.json
├── bank-transfer-service.json
├── budget-service.json
├── management-report-service.json
├── kalene-hills-fruit-company-period-service.json
├── audit-log-service.json
├── current-user-context.json
├── kalene-hills-fruit-company-settings-service.json
├── reconciliation-report-service.json
├── inventory-service.json
├── tax-compliance-service.json
├── product-service.json
├── coa-template-service.json
├── payroll-service.json
├── payroll-journal-service.json
├── tax-filing-service.json
├── contribution-config-service.json
├── customer-service.json
├── crm-account-service.json
├── lead-management-service.json
├── opportunity-service.json
├── contact-service.json
├── campaign-service.json
├── case-management-service.json
├── customer-segmentation-service.json
├── activity-log-service.json
├── social-media-service.json
├── vendor-service.json
├── security-service.json
├── report-export-service.json
├── advanced-reporting-service.json
├── multi-entity-service.json
├── project-costing-service.json
├── financial-modeling-service.json
├── decision-engine-service.json
├── insight-engine-service.json
├── predictive-analytics-service.json
├── realtime-alert-service.json
├── chart-data-service.json
├── referral-service.json
├── place-referral-service.json
├── website-referral-service.json
├── qrcode-service.json
├── promo-code-service.json
├── commission-service.json
├── url-shortener-service.json
├── geocoding-service.json
└── social-sharing-service.json
```

## Mock Services Reference

### Core Kalene Hills Fruit Company Services

#### 1. MockKalene Hills Fruit CompanyService
**File:** `kalene-hills-fruit-company-service.json`
**Purpose:** Main kalene-hills-fruit-company data including accounts, balances, and account types
**Sample Data:** Cash accounts, receivables, payables with Zambian Kwacha (ZMW) currency

#### 2. MockRecurringTransactionService
**File:** `recurring-transaction-service.json`
**Purpose:** Recurring transactions like monthly rent, quarterly taxes
**Sample Data:** Monthly and quarterly recurring payments

#### 3. MockJournalEntryService
**File:** `journal-entry-service.json`
**Purpose:** Double-entry journal entries
**Sample Data:** Office supplies purchase with debit/credit entries

#### 4. MockEstimateService
**File:** `estimate-service.json`
**Purpose:** Customer estimates and quotes
**Sample Data:** Software license estimates with tax calculations

#### 5. MockBankTransferService
**File:** `bank-transfer-service.json`
**Purpose:** Bank account transfers
**Sample Data:** Internal fund transfers between accounts

#### 6. MockBudgetService
**File:** `budget-service.json`
**Purpose:** Budget management and variance analysis
**Sample Data:** Annual budget with operating expenses and revenue

#### 7. MockManagementReportService
**File:** `management-report-service.json`
**Purpose:** Executive management reports
**Sample Data:** Monthly financial summaries with KPIs

#### 8. MockKalene Hills Fruit CompanyPeriodService
**File:** `kalene-hills-fruit-company-period-service.json`
**Purpose:** Fiscal periods and year-end closing
**Sample Data:** Quarterly periods for 2024

#### 9. MockAuditLogService
**File:** `audit-log-service.json`
**Purpose:** Audit trail for all system actions
**Sample Data:** User actions with timestamps

#### 10. MockCurrentUserContext
**File:** `current-user-context.json`
**Purpose:** Current logged-in user context
**Sample Data:** User with roles, permissions, and company info

#### 11. MockKalene Hills Fruit CompanySettingsService
**File:** `kalene-hills-fruit-company-settings-service.json`
**Purpose:** System-wide kalene-hills-fruit-company settings
**Sample Data:** Currency, fiscal year, tax rate settings

#### 12. MockReconciliationReportService
**File:** `reconciliation-report-service.json`
**Purpose:** Bank reconciliation reports
**Sample Data:** Monthly reconciliation with variances

### Inventory & Tax Services

#### 13. MockInventoryService
**File:** `inventory-service.json`
**Purpose:** Inventory tracking and management
**Sample Data:** Office supplies and computer equipment

#### 14. MockTaxComplianceService
**File:** `tax-compliance-service.json`
**Purpose:** Tax compliance and filings
**Sample Data:** VAT and Corporate Tax filings

#### 15. MockProductService
**File:** `product-service.json`
**Purpose:** Product/service catalog
**Sample Data:** Software licenses and consulting services

#### 16. MockCOATemplateService
**File:** `coa-template-service.json`
**Purpose:** Chart of Accounts templates
**Sample Data:** Standard business COA for Zambian businesses

### Payroll Services

#### 17. MockPayrollService
**File:** `payroll-service.json`
**Purpose:** Employee payroll processing
**Sample Data:** Employee payroll with NAPSA, PAYE, NHIMA deductions

#### 18. MockPayrollJournalService
**File:** `payroll-journal-service.json`
**Purpose:** Payroll journal entries
**Sample Data:** Monthly payroll journal with all deductions

#### 19. MockTaxFilingService
**File:** `tax-filing-service.json`
**Purpose:** Tax return filing management
**Sample Data:** VAT return filings

#### 20. MockContributionConfigService
**File:** `contribution-config-service.json`
**Purpose:** Payroll contribution configuration
**Sample Data:** NAPSA and NHIMA contribution rates

### CRM Services

#### 21. MockCustomerService
**File:** `customer-service.json`
**Purpose:** Customer management
**Sample Data:** Zambian businesses with TPIN, contact info

#### 22. MockCrmAccountService
**File:** `crm-account-service.json`
**Purpose:** CRM account tracking
**Sample Data:** Account details with industry, revenue

#### 23. MockLeadManagementService
**File:** `lead-management-service.json`
**Purpose:** Sales lead tracking
**Sample Data:** Potential clients with qualification status

#### 24. MockOpportunityService
**File:** `opportunity-service.json`
**Purpose:** Sales opportunity pipeline
**Sample Data:** Software implementation opportunities

#### 25. MockContactService
**File:** `contact-service.json`
**Purpose:** Contact information management
**Sample Data:** Business contacts with job titles

#### 26. MockCampaignService
**File:** `campaign-service.json`
**Purpose:** Marketing campaign tracking
**Sample Data:** Q1 promotion campaign with budget and conversions

#### 27. MockCaseManagementService
**File:** `case-management-service.json`
**Purpose:** Customer support case management
**Sample Data:** Billing issue cases with priority levels

#### 28. MockCustomerSegmentationService
**File:** `customer-segmentation-service.json`
**Purpose:** Customer segmentation
**Sample Data:** High-value customer segments

#### 29. MockActivityLogService
**File:** `activity-log-service.json`
**Purpose:** CRM activity logging
**Sample Data:** Customer meetings and interactions

#### 30. MockSocialMediaService
**File:** `social-media-service.json`
**Purpose:** Social media integration
**Sample Data:** Twitter and LinkedIn profiles with engagement metrics

#### 31. MockVendorService
**File:** `vendor-service.json`
**Purpose:** Vendor/supplier management
**Sample Data:** Zambian vendors with TPIN and payment terms

### Security & Reporting Services

#### 32. MockSecurityService
**File:** `security-service.json`
**Purpose:** User authentication and authorization
**Sample Data:** User roles, permissions, last login

#### 33. MockReportExportService
**File:** `report-export-service.json`
**Purpose:** Report export functionality
**Sample Data:** PDF and Excel exports of financial reports

#### 34. MockAdvancedReportingService
**File:** `advanced-reporting-service.json`
**Purpose:** Advanced financial reports
**Sample Data:** Cash flow analysis reports

### Multi-Entity & Project Services

#### 35. MockMultiEntityService
**File:** `multi-entity-service.json`
**Purpose:** Multi-entity/subsidiary management
**Sample Data:** Parent and subsidiary entities with different currencies

#### 36. MockProjectCostingService
**File:** `project-costing-service.json`
**Purpose:** Project cost tracking
**Sample Data:** Office renovation project with budget variance

### Financial Modeling Services

#### 37. MockFinancialModelingService
**File:** `financial-modeling-service.json`
**Purpose:** Financial forecasting and modeling
**Sample Data:** 5-year revenue projections

### Decision Engine Services

#### 38. MockDecisionEngineService
**File:** `decision-engine-service.json`
**Purpose:** AI-powered decision support
**Sample Data:** Credit approval decisions with confidence scores

#### 39. MockInsightEngineService
**File:** `insight-engine-service.json`
**Purpose:** Business insights generation
**Sample Data:** Revenue trend insights with action items

#### 40. MockPredictiveAnalyticsService
**File:** `predictive-analytics-service.json`
**Purpose:** Predictive analytics
**Sample Data:** 3-month cash flow forecasts

#### 41. MockRealtimeAlertService
**File:** `realtime-alert-service.json`
**Purpose:** Real-time alerts and notifications
**Sample Data:** Low cash balance and overdue invoice alerts

#### 42. MockChartDataService
**File:** `chart-data-service.json`
**Purpose:** Chart and visualization data
**Sample Data:** Revenue by month comparison charts

### MLM/Referral Services

#### 43. MockReferralService
**File:** `referral-service.json`
**Purpose:** Customer referral tracking
**Sample Data:** Referral conversions with commissions

#### 44. MockPlaceReferralService
**File:** `place-referral-service.json`
**Purpose:** Location-based referrals
**Sample Data:** Shopping mall referral locations with GPS coordinates

#### 45. MockWebsiteReferralService
**File:** `website-referral-service.json`
**Purpose:** Website-based referrals
**Sample Data:** Website referral campaigns with conversion rates

#### 46. MockQRCodeService
**File:** `qrcode-service.json`
**Purpose:** QR code generation and tracking
**Sample Data:** QR codes with scan and conversion metrics

#### 47. MockPromoCodeService
**File:** `promo-code-service.json`
**Purpose:** Promotional code management
**Sample Data:** Discount codes with usage limits

#### 48. MockCommissionService
**File:** `commission-service.json`
**Purpose:** Affiliate commission tracking
**Sample Data:** Monthly commission calculations

#### 49. MockUrlShortenerService
**File:** `url-shortener-service.json`
**Purpose:** URL shortening for marketing
**Sample Data:** Short URLs with click tracking

#### 50. MockGeocodingService
**File:** `geocoding-service.json`
**Purpose:** Address to GPS coordinate conversion
**Sample Data:** Lusaka addresses with lat/long coordinates

#### 51. MockSocialSharingService
**File:** `social-sharing-service.json`
**Purpose:** Social media sharing tracking
**Sample Data:** Blog post shares on Facebook and LinkedIn

## Usage

### Configuration

The Kalene Hills Fruit CompanyWebApp uses these mock services when configured with `UseMockServices: true` in `appsettings.json`:

```json
{
  "ServiceConfiguration": {
    "UseMockServices": true,
    "ApiGatewayUrl": "https://localhost:5000"
  }
}
```

### Service Registration

All mock services are registered in `ServiceCollectionExtensions.cs` using the `AddAdvancedKalene Hills Fruit CompanyMockServices()` extension method.

### Data Format

All JSON files follow a consistent structure:
- Arrays for list-based data
- Objects for single-record data (like settings and current user context)
- Zambian-specific data where applicable (currency: ZMW, TPIN tax IDs, Lusaka addresses)

## Zambian Context

Sample data includes Zambian-specific elements:
- **Currency:** ZMW (Zambian Kwacha)
- **Tax IDs:** TPIN (Taxpayer Identification Number)
- **Locations:** Lusaka addresses (Cairo Road, Independence Avenue, etc.)
- **Payroll Deductions:** NAPSA, PAYE, NHIMA
- **Tax Rates:** 16% VAT, 35% Corporate Tax

## Extending Sample Data

To add or modify sample data:

1. Locate the appropriate JSON file in `wwwroot/sample-data/kalene-hills-fruit-company/`
2. Follow the existing data structure
3. Ensure all required fields are populated
4. Use realistic Zambian business data where applicable
5. Maintain data consistency across related files (e.g., customer IDs should match across services)

## Dependencies

The number of JSON files (51) matches the number of Mock Services registered in:
- **File:** `src/Components/Components.Advanced.Accounts/ServiceCollectionExtensions.cs`
- **Method:** `AddAdvancedKalene Hills Fruit CompanyServices(useMockData: true)`

## Related Documentation

- [Service Configuration Guide](../../SERVICE_CONFIGURATION_GUIDE.md)
- [Mock to HTTP Services Analysis](../../MOCK_TO_HTTP_SERVICES_ANALYSIS.md)
- [Advanced Accounts Integration](../../ADVANCED_ACCOUNTS_INTEGRATION.txt)

## Support

For questions or issues with sample data, please refer to the main project documentation or contact the development team.

---

**Last Updated:** January 2024
**Version:** 1.0
**Maintained By:** Kalene Hills Fruit Company-As-A-Service Development Team
