# Customer-Facing Booking Website

## Overview

A public-facing Blazor WebAssembly website for tourists to browse packages, book accommodation, reserve activities, purchase park permits, and manage their travel plans. This is a **frontend-only** application that consumes the **AccountingWebApp API** for all cross-cutting concerns (CRM, invoicing, payments, tax). It does NOT duplicate any AccountingWebApp features.

---

## Tech Stack

| Layer | Technology |
|---|---|
| **Framework** | .NET 10 Blazor WebAssembly |
| **UI Library** | Radzen.Blazor v10+ (Material Design) |
| **CSS** | Bootstrap 5 + Custom tourism theme |
| **Icons** | Material Icons |
| **Auth** | JWT (via AccountingWebApp `IAuthenticationService` API) |
| **Maps** | Leaflet.js (open-source map display for lodges/parks) |
| **Backend** | AccountingWebApp API Gateway (single shared backend) |

---

## Architecture

```
Customer Website (Blazor WASM — frontend ONLY)
       │
       │  ALL data & logic comes from the shared backend
       ▼
AccountingWebApp API Gateway  ◄── Backend Management Website also consumes this
       │
       ├── Booking-specific endpoints (/api/bookings, /api/packages, /api/accommodation...)
       ├── Existing AccountingWebApp services (CRM, Invoicing, Tax, Payments, Reports...)
       └── Authentication & Authorization
```

**Key rule:** The customer website NEVER manages CRM profiles, invoices, payments, or tax directly. It calls the AccountingWebApp API which handles all business logic, accounting entries, and financial records.

---

## What the Customer Website Does (Unique Tourism Features)

| Area | Description | Shared via API |
|---|---|---|---|
| Travel Deals | Special offers, last-minute discounts, early-bird promotions, bundle deals | Custom Booking API |
| Holiday Packages | Pre-configured holiday bundles by season (Summer, Winter, Easter, Christmas) | Custom Booking API |
| Corporate Travel | Corporate account booking, negotiated rates, bulk invoicing | Custom Booking API |
| Group Bookings | Book for 10+ guests, group discounts, room allocation | Custom Booking API |
| Browse tourism packages | Listing + detail for Bronze/Silver/Gold/Platinum packages | Custom Booking API |
| Search accommodation | Room/lodge/campsite availability and pricing | Custom Booking API |
| Book activities | Game drives, safaris, boat cruises, permits | Custom Booking API |
| Park entry passes | Daily/weekly/annual passes with citizen/resident/international pricing | Custom Booking API |
| Destinations | Browse by destination/park with highlights, wildlife, and lodging options | Custom Booking API |
| Shopping cart | Multi-item booking cart with summary | Custom Booking API |
| Booking management | View/modify/cancel own bookings | Custom Booking API |
| Customer profile | Basic profile read-only (master data in AccountingWebApp CRM) | AccountingWebApp CRM API |
| Loyalty points display | View points balance and history | AccountingWebApp CRM API |
| Invoice download | View/download invoices for completed bookings | AccountingWebApp Invoicing API |
| Make payments | Pay via integrated payment gateway | AccountingWebApp Payments API |
| Products & Services | Browse all offerings: accommodation, activities, permits, transfers, merchandise | Custom Booking API |
| Travellers Guide | Pre-trip info: packing lists, health & safety, park rules, travel tips | Static / CMS |
| Visa Free Countries | List of visa-free countries for park entry, visa application guides, embassy info | Static / CMS |

---

## What Is NOT Rebuilt (Already in AccountingWebApp)

| Feature | Where It Lives | How Customer Site Uses It |
|---|---|---|
| Customer/Contact management | AccountingWebApp CRM | Links to `/crm/contacts/{id}` |
| Invoice generation | AccountingWebApp Sales & Receivables | Links to `/invoice/{id}` |
| Payment processing | AccountingWebApp Payments API | Redirects to payment flow |
| Tax calculation | AccountingWebApp Tax module | Prices fetched with tax included via API |
| Reporting & analytics | AccountingWebApp Reports | Not accessible from customer site |
| User/role management | AccountingWebApp Settings | Handled by AccountingWebApp admins |
| AI Assistant | AccountingWebApp AI Assistant | Not exposed to customers |

---

## Pages

### 1. Home Page
- **Route:** `/`
- **Auth:** None
- Hero banner with destination imagery
- Search bar: destination / dates / guests
- Travel Deals carousel (limited-time offers, last-minute discounts)
- Featured packages grid
- Quick links: Park Entry, Game Drives, Accommodation
- Testimonial carousel
- **API:** `GET /api/bookings/travel-deals`

### 2. Travel Deals
- **Route:** `/travel-deals`
- **Auth:** None
- Special offers grid: limited-time discounts, early-bird rates, last-minute deals
- Countdown timers on expiring deals
- Seasonal promotions (Summer Safari, Winter Escape, Christmas Special)
- Bundle deals (stay + game drive + meals at reduced rate)
- **API:** `GET /api/bookings/travel-deals`

### 3. Holiday Packages
- **Route:** `/holiday-packages`
- **Auth:** None
- Pre-configured holiday bundles by season: Summer Safari, Winter Wildlife, Easter Break, Christmas/New Year
- Filter by season, duration, price
- Family-friendly and honeymoon holiday options
- All-inclusive pricing display
- **API:** `GET /api/bookings/holiday-packages`

### 4. Destinations
- **Route:** `/destinations`
- **Auth:** None
- Destination cards with imagery, highlights, wildlife species, best visiting season
- Map view (Leaflet.js) showing all parks/lodges
- Filter by region, wildlife, activities available
- Quick links to accommodation and activities in each destination
- **API:** `GET /api/bookings/destinations`

### 5. Corporate Travel
- **Route:** `/corporate-travel`
- **Auth:** None (booking requires corporate account)
- Corporate account registration form
- Negotiated rates display (login required)
- Corporate booking portal: book for employees/clients
- Bulk invoicing option
- Dedicated corporate support contact
- **API:** `GET /api/bookings/corporate`, `POST /api/bookings/corporate/register`

### 6. Group Bookings
- **Route:** `/group-bookings`
- **Auth:** None (booking requires contact)
- Group booking inquiry form (10+ guests)
- Group discount tiers
- Room allocation preview
- Group payment options (single payer or individual)
- School / NGO / Conference group options
- **API:** `POST /api/bookings/group/inquiry`, `GET /api/bookings/group/pricing`

### 7. Products & Services
- **Route:** `/products-services`
- **Auth:** None
- Browse all tourism products: accommodation, activities, permits, transfers, merchandise, curated packages
- Service comparison tool
- Availability calendar (summary view across all products)
- **API:** `GET /api/bookings/products`

### 8. Travellers Guide
- **Route:** `/travellers-guide`
- **Auth:** None
- Pre-trip checklists (packing list, required documents, vaccinations)
- Park rules and regulations
- Health & safety information
- Wildlife viewing tips
- Cultural etiquette
- FAQ section
- Downloadable PDF guides
- **Data:** Static content / CMS-managed markdown (no accounting API)

### 9. Visa Free Countries
- **Route:** `/visa-free`
- **Auth:** None
- Searchable list of visa-free and visa-on-arrival countries
- Visa application guides for countries requiring visas
- Embassy and consulate information
- Passport validity requirements
- Entry fee information (linked to park entry pricing)
- **Data:** Static content / CMS-managed (no accounting API)

### 10. Packages
- **Route:** `/packages`
- **Auth:** None
- Package cards: Bronze, Silver, Gold, Platinum
- Filter by category, price range, duration
- Seasonal/peak pricing badges
- **API:** `GET /api/bookings/packages`

### 11. Package Detail
- **Route:** `/packages/{id}`
- **Auth:** None
- Itinerary, inclusions, exclusions
- Available dates calendar
- Pricing table (peak/off-peak)
- "Book Now" button → cart

### 12. Accommodation Search
- **Route:** `/accommodation`
- **Auth:** None
- Lodge/campsite listing with map view
- Room types: Standard, Deluxe, Executive, Presidential
- Lodge types: Chalets, Tents, Villas, Cabins
- Filter: capacity, amenities, price
- **API:** `GET /api/bookings/accommodation`

### 13. Accommodation Detail
- **Route:** `/accommodation/{id}`
- **Auth:** None
- Photo gallery
- Amenities list
- Rate calendar (per-night)
- Booking form: check-in, check-out, guests, add-ons
- **API:** `GET /api/bookings/accommodation/{id}/availability`

### 14. Activities
- **Route:** `/activities`
- **Auth:** None
- Activity listings: Game Drives, Walking Safaris, Night Drives, Bird Watching, Canoeing, Fishing, Horse Riding, Cultural Tours, Conservation Tours
- Filter by type, duration, capacity
- **API:** `GET /api/bookings/activities`

### 15. Activity Booking
- **Route:** `/activities/{id}/book`
- **Auth:** Required
- Select date/time slot
- Number of participants
- Add to cart
- **API:** `POST /api/bookings/activities/{id}/check-availability`

### 16. Park Entry
- **Route:** `/park-entry`
- **Auth:** None
- Park selection (map + list)
- Pricing by visitor category (Citizen/Resident/International, Adult/Child/Student/Senior)
- Pass types: Daily, Weekly, Annual
- Additional fees: Vehicle, Boat, Drone Permit, Photography Permit, Film Permit
- **API:** `GET /api/bookings/park-entry/pricing`

### 17. Cart / Checkout
- **Route:** `/cart`
- **Auth:** Optional (guest checkout with email)
- Line items with dates, quantities, pricing
- Promo code / discount
- Customer details → stored as Contact via AccountingWebApp CRM API
- Payment method selection (redirects to AccountingWebApp payment flow)
- **API:**
  - `POST /api/crm/contacts` (via AccountingWebApp CRM)
  - `POST /api/bookings` (creates booking → auto-generates invoice via AccountingWebApp Invoicing)

### 18. My Bookings
- **Route:** `/my-bookings`
- **Auth:** Required
- List of current/past bookings
- Download invoice (PDF from AccountingWebApp Invoicing API)
- Cancel / modify booking
- **API:** `GET /api/bookings/my-bookings`, `POST /api/bookings/{id}/cancel`

### 19. Booking Confirmation
- **Route:** `/booking/{reference}/confirmation`
- **Auth:** None (access via reference code)
- Booking reference
- Payment summary
- Link to invoice (hosted on AccountingWebApp)
- **API:** `GET /api/bookings/{reference}`

### 20. Login / Register
- **Route:** `/login`, `/register`
- **Auth:** None
- Calls AccountingWebApp `IAuthenticationService` API
- Registration creates Contact in AccountingWebApp CRM
- Social login options (Google, Facebook)
- **API:** `POST /api/auth/login`, `POST /api/auth/register`

### 21. Profile
- **Route:** `/profile`
- **Auth:** Required
- Display personal details (read from AccountingWebApp CRM)
- Travel history (from Bookings API)
- Loyalty points display (from AccountingWebApp CRM)
- Preferences
- **API:** `GET /api/crm/contacts/{id}` (via AccountingWebApp CRM)

### 22. Loyalty
- **Route:** `/loyalty`
- **Auth:** Required
- Points balance and tier status
- Points history
- Available rewards
- **API:** `GET /api/crm/loyalty/points` (via AccountingWebApp CRM)

---

## API Endpoints Consumed (All from AccountingWebApp Gateway)

```
# Booking-specific endpoints
GET    /api/bookings/travel-deals
GET    /api/bookings/holiday-packages
GET    /api/bookings/destinations
GET    /api/bookings/corporate
POST   /api/bookings/corporate/register
GET    /api/bookings/group/pricing
POST   /api/bookings/group/inquiry
GET    /api/bookings/products
GET    /api/bookings/packages
GET    /api/bookings/packages/{id}
GET    /api/bookings/accommodation
GET    /api/bookings/accommodation/{id}/availability
GET    /api/bookings/activities
POST   /api/bookings/activities/{id}/check-availability
GET    /api/bookings/park-entry/pricing
POST   /api/bookings                    (create booking → auto-generates invoice)
GET    /api/bookings/my-bookings
GET    /api/bookings/{reference}
POST   /api/bookings/{id}/cancel

# AccountingWebApp shared services (NOT rebuilt)
POST   /api/auth/login                  (IAuthenticationService)
POST   /api/auth/register
GET    /api/crm/contacts/{id}           (CRM - read customer profile)
GET    /api/crm/loyalty/points          (CRM - loyalty display)
GET    /api/invoices/{id}/pdf           (Invoicing - download PDF)
POST   /api/payments/checkout           (Payments - initiate payment)
```

---

## UI Points

- **Theme:** Warm earth tones (#8B5E3C, #2D5016, #C9A84C), safari/nature feel
- **Responsive:** Mobile-first (most tourists browse on phones)
- **Performance:** Lazy-loaded images, Brotli WASM compression, preloaded critical assets
- **Guest checkout:** Email verification, no account required for booking
