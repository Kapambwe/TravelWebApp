# Travel Booking Website — UI Look & Feel

## Design Inspiration

Combining **Booking.com's** masterclass in search simplicity with **TravelStart's** African travel market expertise. Clean, professional, dynamic — not bloated.

---

## Color Palette

| Token | Hex | Usage |
|---|---|---|
| Primary | `#003B95` | Header, buttons, links (Booking.com blue) |
| Primary Dark | `#002A6E` | Hover states, active tab |
| Primary Light | `#E8F0FE` | Search bar bg, card highlights |
| Accent | `#FEBB02` | Deal badges, price highlights, star ratings (TravelStart gold) |
| Success | `#008A0E` | Available, confirmed, check-in ready |
| Danger | `#D32F2F` | Sold out, cancellation, alerts |
| Text Primary | `#1A1A2E` | Headings, body text |
| Text Secondary | `#5A5A7A` | Labels, descriptions, metadata |
| Surface | `#FFFFFF` | Cards, modals, dropdowns |
| Background | `#F5F7FA` | Page background |
| Border | `#E0E4EC` | Dividers, input borders |

---

## Typography

| Element | Radzen Equivalent | Size | Weight |
|---|---|---|---|
| Hero heading | `RadzenText TextStyle="Display1"` | 2.5rem | 700 |
| Section title | `RadzenText TextStyle="H4"` | 1.5rem | 600 |
| Card title | `RadzenText TextStyle="H5"` | 1.125rem | 600 |
| Body | `RadzenText TextStyle="Body1"` | 0.938rem | 400 |
| Caption | `RadzenText TextStyle="Caption"` | 0.813rem | 400 |
| Price | `RadzenText TextStyle="H5"` | 1.25rem | 700 |

Font family: `Inter, -apple-system, BlinkMacSystemFont, Segoe UI, sans-serif`

---

## Layout Structure

```
┌─────────────────────────────────────────────────────────┐
│  TopBar (RadzenPanelMenu collapsed to hamburger on mob) │
│  Logo | Flights | Hotels | Packages | Deals | AI Agent  │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  ┌─────────────────────────────────────────────────┐   │
│  │  HERO SECTION (full-width, gradient background)  │   │
│  │                                                   │   │
│  │  "Where to next?"                                 │   │
│  │  [Destination ▼]  [Check-in ⇄ Check-out] [Guests]│   │
│  │  [              Search Button              ]      │   │
│  │                                                   │   │
│  │  Quick links: Last Minute | Weekend | Safari      │   │
│  └─────────────────────────────────────────────────┘   │
│                                                         │
│  ┌─────────────────────────────────────────────────┐   │
│  │  TOP DESTINATIONS (RadzenRow x4 cards)          │   │
│  │  ┌────┐ ┌────┐ ┌────┐ ┌────┐                   │   │
│  │  |Cap | |Joburg| |Vic | |Kruger|               │   │
│  │  |Town| |      | |Falls|       |               │   │
│  │  └────┘ └────┘ └────┘ └────┘                   │   │
│  └─────────────────────────────────────────────────┘   │
│                                                         │
│  ┌─────────────────────────────────────────────────┐   │
│  │  TRAVEL DEALS (RadzenCarousel with cards)       │   │
│  │  ┌──────────┐ ┌──────────┐ ┌──────────┐        │   │
│  │  | -40%    | | Early    | | Flash    |        │   │
│  │  | Safari  | | Bird     | | Sale     |        │   │
│  │  └──────────┘ └──────────┘ └──────────┘        │   │
│  └─────────────────────────────────────────────────┘   │
│                                                         │
│  ┌─────────────────────────────────────────────────┐   │
│  │  BOOK BY EXPERIENCE (icon row)                  │   │
│  │  🏖️ Safari  🏔️ Hiking  🐘 Wildlife  🏝️ Beach    │   │
│  └─────────────────────────────────────────────────┘   │
│                                                         │
│  ┌─────────────────────────────────────────────────┐   │
│  │  AI TRAVEL ASSISTANT (floating chat FAB)        │   │
│  │  "Ask Otto" — always bottom-right               │   │
│  └─────────────────────────────────────────────────┘   │
│                                                         │
│  Footer                                                │
│  Logo | About | Contact | T&Cs | Currency Selector     │
└─────────────────────────────────────────────────────────┘
```

---

## Pages

### 1. Home Page (`/`)

**Hero Search Section** (Booking.com inspired — minimal, centered)
```
┌─────────────────────────────────────────────────────┐
│  Background: gradient image overlay                 │
│                                                     │
│           "Where will your next adventure take you?" │
│                                                     │
│  ┌──────────┐  ┌──────────────────┐  ┌──────────┐  │
│  │ Cape     │  │ Check-in ▸       │  │ 2 Adults │  │
│  │ Town ▼   │  │ Check-out        │  │ 1 Room ▼ │  │
│  └──────────┘  └──────────────────┘  └──────────┘  │
│                                                     │
│  ┌──────────────────────────────────────────────┐   │
│  │            🔍  Search                        │   │
│  └──────────────────────────────────────────────┘   │
│                                                     │
│  Quick filters: Last Minute | Weekend Getaways |    │
│  Safari Packages | Beach Holidays                   │
└─────────────────────────────────────────────────────┘
```

**Radzen components used:**
- `RadzenTextBox` with autocomplete for destination
- `RadzenDateRangePicker` for check-in/check-out (single combined field)
- `RadzenDropDown` for guest/room count
- `RadzenButton` full-width, rounded, primary with shadow

**Top Destinations Grid**
```
┌─────────────┐ ┌─────────────┐ ┌─────────────┐ ┌─────────────┐
│ 🏙️ Cape    │ │ 🏙️ Johannes-│ │ 🌊 Victoria │ │ 🦁 Kruger   │
│   Town      │ │   burg      │ │   Falls     │ │   National  │
│ From R850   │ │ From R650   │ │ From R1,200 │ │ From R2,400 │
│ /night      │ │ /night      │ │ /night      │ │ /night      │
└─────────────┘ └─────────────┘ └─────────────┘ └─────────────┘
```

**Radzen components:** `RadzenRow` + `RadzenColumn Size="3"` with `RadzenCard` inside each column showing destination image, name, and starting price.

**Travel Deals Carousel**
```
◄─── [ -40% Safari Special! ] [ Early Bird: 15% Off ] [ Flash Sale: 3 Days Left ] ───►
        Book by 30 June                Book 60+ days ahead            Limited availability
```

**Radzen components:** `RadzenCarousel` with `RadzenCard` items showing deal badge (accent color), title, description, countdown timer.

**Book by Experience (icon strip)**
| Safari | Beach | Hiking | Wildlife | Cultural | Adventure |
|---|---|---|---|---|---|
Each is a `RadzenButton` with `ButtonStyle="ButtonStyle.Link"` and an icon, clicking navigates to `/search?experience=safari`.

---

### 2. Search Results (`/search`)

```
┌─────────────────────────────────────────────────────────┐
│  Filters (left sidebar, RadzenPanelMenu style)          │
│                                                         │
│  Price Range                                             │
│  [=========o======]  R500 — R3,000                      │
│                                                         │
│  Property Type                                           │
│  ☑ Hotel (24)  ☐ Lodge (12)  ☐ Chalet (8)              │
│  ☐ Campsite (5) ☐ Villa (3)                             │
│                                                         │
│  Star Rating                                             │
│  ★★★★☆  4+ stars (18)                                  │
│  ★★★☆☆  3+ stars (32)                                  │
│                                                         │
│  Free Cancellation  ☐                                   │
│  Breakfast Included ☐                                   │
│  Pool               ☐                                   │
│                                                         │
│  Review Score                                            │
│  ☑ Wonderful 9+ (12)  ☐ Very Good 8+ (24)              │
│                                                         │
│  Budget Friendly                                         │
│  [  Apply Filters  ]   [  Clear All  ]                  │
│                                                         │
├─────────────────────────────────────────────────────────┤
│  Results (main area)                                    │
│                                                         │
│  "Cape Town: 47 places found"  [Sort by: Price ▼]      │
│                                                         │
│  ┌─────────────────────────────────────────────────┐    │
│  │ [IMG]  The Table Bay Hotel                      │    │
│  │        ★★★★☆  4.8 (234 reviews)                 │    │
│  │        Waterfront, Cape Town                    │    │
│  │        🅿️ 🏊 🍳 Free WiFi                       │    │
│  │        ✓ Free cancellation                     │    │
│  │                              ~~R2,400~~ R1,800  │    │
│  │                              per night         │    │
│  │                              [  View Deal  ]   │    │
│  └─────────────────────────────────────────────────┘    │
│                                                         │
│  ┌─────────────────────────────────────────────────┐    │
│  │ [IMG]  Gorgeous George                          │    │
│  │        ★★★★☆  4.6 (187 reviews)                 │    │
│  │        City Centre, Cape Town                   │    │
│  │        🅿️ 🍳 Rooftop Pool                       │    │
│  │                                  R1,350         │    │
│  │                                  per night      │    │
│  │                                  [  View Deal  ] │    │
│  └─────────────────────────────────────────────────┘    │
│                                                         │
│  [Load More Results]  (infinite scroll or pagination)   │
└─────────────────────────────────────────────────────────┘
```

**Radzen components used:**
- `RadzenSidebar` / `RadzenPanelMenu` for filter panel
- `RadzenSlider` for price range
- `RadzenCheckBoxList` for property type / amenities
- `RadzenRating` for star filter
- `RadzenCard` for each result item
- `RadzenBadge` for discount badges
- `RadzenDropDown` for sort selector
- `RadzenPager` for pagination

**Result card patterns:**
- Image left, content right (desktop) / stacked (mobile)
- Hotel name, star rating, review score with count
- Location, amenity icons
- Original price strikethrough + discounted price in accent color
- "Free cancellation" green badge
- "View Deal" button in primary color, rounded

---

### 3. Property / Package Detail (`/detail/{id}`)

```
┌─────────────────────────────────────────────────────────┐
│  Image Gallery (RadzenCarousel full-width)              │
│  [←]  img1  img2  img3  img4  img5  [→]               │
│  ● ● ○ ○ ○  (dot indicators)                           │
├─────────────────────────────────────────────────────────┤
│                                                         │
│  ┌─Main Content (left 70%)──┐  ┌─Booking Card (right)─┐│
│  │ The Table Bay Hotel      │  │  ★★★★☆               ││
│  │ ★★★★☆  4.8 (234 reviews) │  │                       ││
│  │ Waterfront, Cape Town    │  │  Check-in ▸ Check-out ││
│  │                          │  │  [Date Range Picker]  ││
│  │ About this property      │  │                       ││
│  │ Luxury 5-star hotel...   │  │  Guests               ││
│  │                          │  │  [2 Adults, 1 Room ▼] ││
│  │ Amenities                │  │                       ││
│  │ 🏊 Pool  🅿️ Parking      │  │  ~~R2,400~~ R1,800   ││
│  │ 🍳 Breakfast  WiFi  Gym  │  │  per night            ││
│  │                          │  │  Total: R5,400        ││
│  │ What's included          │  │                       ││
│  │ ✓ Free cancellation      │  │  [  Book Now  ]      ││
│  │ ✓ Breakfast included     │  │                       ││
│  │ ✓ Airport transfers     │  │  🔒 Secure payment   ││
│  │                          │  │                       ││
│  │ Reviews                  │  └───────────────────────┘│
│  │ "Amazing stay!" ★★★★★   │                           │
│  │ "Perfect location" ★★★★☆ │                           │
│  └──────────────────────────┘                           │
└─────────────────────────────────────────────────────────┘
```

**Radzen components used:**
- `RadzenCarousel` for image gallery
- `RadzenRow` + `RadzenColumn Size="8"` + `Size="4"` for layout
- `RadzenCard` for booking card (sticky on scroll)
- `RadzenDateRangePicker` for date selection
- `RadzenNumeric` for guest count
- `RadzenRating` for star display
- `RadzenText` for descriptions
- `RadzenBadge` for included features
- `RadzenButton` primary-large for "Book Now"
- `RadzenProgressBarCircular` for loading states

**Booking card (sticky sidebar):**
- Shows real-time price calculation as user changes dates/guests
- "Book Now" button is prominent, full-width, with lock icon
- Below: trust signals (secure payment, no hidden fees)

---

### 4. Booking Flow (`/checkout`)

Multi-step wizard in a centered card:

```
Step 1: Your Details        Step 2: Extras        Step 3: Payment
[●────────○────────○]    [○────────●────────○]  [○────────○────────●]

┌─────────────────────────────────────────────────────────┐
│  Step 1: Your Details                                    │
│                                                         │
│  Full Name        [________________]                    │
│  Email            [________________]                    │
│  Phone            [________________]                    │
│  ID/Passport      [________________]                    │
│                                                         │
│  Special requests [________________]                    │
│                                                         │
│  [  Back  ]                         [  Continue  ]      │
└─────────────────────────────────────────────────────────┘
```

**Radzen components:** `RadzenSteps` / `RadzenTemplateForm` with `RadzenTextBox`, `RadzenRequiredValidator`, `RadzenButton`.

---

### 5. My Bookings (`/my-bookings`)

```
┌─────────────────────────────────────────────────────────┐
│  My Bookings                                            │
│                                                         │
│  ┌─ Upcoming ───────┐  ┌─ Past ───────────┐           │
│  │ Safari Package   │  │ Cape Town Hotel  │           │
│  │ 12-15 Jun 2026   │  │ 2-5 Mar 2026     │           │
│  │ Confirmed ✓      │  │ Completed ✓      │           │
│  │ [View] [Cancel]  │  │ [Review]         │           │
│  └──────────────────┘  └──────────────────┘            │
└─────────────────────────────────────────────────────────┘
```

**Radzen components:** `RadzenTabs` (Upcoming / Past / Cancelled), `RadzenCard` for each booking, `RadzenBadge` for status.

---

### 6. AI Travel Assistant (`/ai-agent`)

Floating chat widget + full-page mode:

```
┌──────────────────┐
│  🤖 Otto        │  ← Floating action button, bottom-right
│  Ask me anything │     Always visible across all pages
│  about your trip │
└──────────────────┘

┌──────────────────────────────────────┐
│  Otto — Your AI Travel Assistant     │
│                                      │
│  ┌──────────────────────────────┐    │
│  │ 💬 Find flights from Cape   │    │
│  │    Town to Victoria Falls   │    │
│  │    on 15 July              │    │
│  └──────────────────────────────┘    │
│                                      │
│  ┌──────────────────────────────┐    │
│  │ ✈️ Found 3 flights:         │    │
│  │ 1. 07:00 - 09:15  R1,850   │    │
│  │ 2. 11:30 - 13:45  R2,100   │    │
│  │ 3. 16:00 - 18:20  R1,650   │    │
│  └──────────────────────────────┘    │
│                                      │
│  [Type your message...] [Send ►]     │
└──────────────────────────────────────┘
```

**Radzen components:** `RadzenChat` (custom), `RadzenFloatingButton` for the FAB, `RadzenTextBox` for input, `RadzenBadge` for unread count.

---

### 7. Travellers Guide (`/guide`)

```
┌─────────────────────────────────────────────────────────┐
│  Travellers Guide                                       │
│                                                         │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐  │
│  │ Packing  │ │  Visa    │ │  Health  │ │  Park    │  │
│  │ List     │ │  Guide   │ │ & Safety │ │  Rules   │  │
│  └──────────┘ └──────────┘ └──────────┘ └──────────┘  │
│                                                         │
│  ┌─────────────────────────────────────────────────┐    │
│  │ Visa-Free Countries                             │    │
│  │ Search: [________________]                      │    │
│  │                                                 │    │
│  │ 🇿🇦 South Africa → 🇧🇼 Botswana (90 days)       │    │
│  │ 🇿🇦 South Africa → 🇿🇲 Zambia (90 days)         │    │
│  │ 🇿🇦 South Africa → 🇰🇪 Kenya (30 days)          │    │
│  └─────────────────────────────────────────────────┘    │
└─────────────────────────────────────────────────────────┘
```

**Radzen components:** `RadzenCard` grid for guide sections, `RadzenDataGrid` for visa-free countries, `RadzenTextBox` with search.

---

## Component Library (Radzen Mapping)

| UI Element | Radzen Component | Customization |
|---|---|---|
| Search bar destination | `RadzenTextBox` + autocomplete via `LoadData` | Rounded borders, large padding, shadow on focus |
| Date range picker | `RadzenDateRangePicker` | Single field showing "Check-in ▸ Check-out", flat design |
| Guest selector | `RadzenDropDown` | Custom template showing adults/children/rooms count |
| Property cards | `RadzenCard` + `RadzenRow` | Hover elevation increase, border-radius: 12px |
| Price display | `RadzenText` + `RadzenBadge` | Accent color for price, strikethrough for original |
| Star rating | `RadzenRating` | Read-only, gold stars |
| Image gallery | `RadzenCarousel` | Full-width, dot indicators, arrow navigation |
| Filter sidebar | `RadzenPanelMenu` + `RadzenPanelMenuItem` | First-level expandable filter groups |
| Price slider | `RadzenSlider` | Range mode, track in primary color |
| Checkboxes | `RadzenCheckBox` | Custom styled with larger touch targets |
| Buttons | `RadzenButton` | `BorderRadius="8px"`, `ButtonStyle="Primary"`, full-width on mobile |
| Stepper | `RadzenSteps` | Step indicator with icons, linear flow |
| Form inputs | `RadzenTextBox` / `RadzenNumeric` | With `RadzenRequiredValidator` for validation |
| Tabs | `RadzenTabs` | For My Bookings (Upcoming/Past/Cancelled) |
| Badges | `RadzenBadge` | `BadgeStyle.Success` for available, `Danger` for sold out |
| Dialogs | `RadzenDialog` | For cancellation confirmation, payment success |
| Notifications | `NotificationService` | Toast for booking confirmation, errors |
| AI Chat | Custom with `RadzenCard` + `RadzenTextBox` | Floating FAB opens chat panel |
| Header/Nav | `RadzenPanelMenu` | Collapsible hamburger on mobile, horizontal on desktop |
| Footer | `RadzenRow` with social links | Dark background, light text |

---

## Radzen Theme Customization

```xml
<!-- app.css overrides -->
<style>
  .rz-card { border-radius: 12px; border: 1px solid #E0E4EC; transition: box-shadow 0.2s; }
  .rz-card:hover { box-shadow: 0 8px 24px rgba(0,59,149,0.1); }
  .rz-button { border-radius: 8px; font-weight: 600; padding: 12px 24px; }
  .rz-button.rz-primary { background: #003B95; }
  .rz-button.rz-primary:hover { background: #002A6E; }
  .rz-textbox { border-radius: 8px; border: 1px solid #E0E4EC; padding: 12px 16px; }
  .rz-textbox:focus { border-color: #003B95; box-shadow: 0 0 0 3px rgba(0,59,149,0.15); }
  .rz-daterangepicker { border-radius: 8px; border: 1px solid #E0E4EC; }
  .rz-badge-success { background: #008A0E; }
  .rz-rating { color: #FEBB02; }
  .rz-panelmenu { border: none; }
  .rz-panelmenu .rz-navigation-item-text { font-weight: 500; }
</style>
```

---

## Page Inventory (Lite)

| # | Page | Route | Dynamic Content |
|---|---|---|---|
| 1 | Home | `/` | Destinations, deals, hero search |
| 2 | Search Results | `/search` | Filterable results grid |
| 3 | Property Detail | `/detail/{id}` | Gallery, booking card, reviews |
| 4 | Checkout | `/checkout` | 3-step wizard (details, extras, payment) |
| 5 | Booking Confirmation | `/confirmation/{ref}` | Booking summary, download invoice |
| 6 | My Bookings | `/my-bookings` | Tabbed booking list |
| 7 | Travellers Guide | `/guide` | Visa info, packing lists, park rules |
| 8 | AI Travel Assistant | `/ai-agent` | Chat interface |
| 9 | Login / Register | `/login`, `/register` | Auth forms |
| 10 | Profile | `/profile` | Personal details, preferences |

10 pages — lite but complete travel journey from discovery to booking to post-trip.

---

## Responsive Breakpoints

| Breakpoint | Columns | Search Layout | Cards per Row |
|---|---|---|---|
| >1200px | 12 | Side-by-side (filters left, results right) | 4 destinations, 2 results |
| 768-1199px | 12 | Filters as collapsible drawer | 2 destinations, 1 result |
| <768px | 12 | Filters in overlay modal | 1 card per row, stacked |

---

## Key UX Patterns

1. **Search-first**: Hero search is always the most prominent element on home
2. **Smart defaults**: 2 adults, 1 room, tonight-tomorrow dates
3. **Progressive disclosure**: Show basic filters first, "More filters" expands
4. **Price anchoring**: Show strikethrough original price next to discounted
5. **Social proof**: Review scores, count of reviews, star ratings on every card
6. **Urgency signals**: "Only 2 rooms left", "23 people viewing", countdown timers on deals
7. **Free cancellation**: Green badge on eligible properties (top conversion driver)
8. **Persistent AI assistant**: Floating Otto button always available
9. **Sticky booking card**: On detail page, booking card follows scroll
10. **Guest checkout**: No account required until payment step

---

## Micro-Interactions (for `tourism.js`)

| Interaction | Behavior |
|---|---|
| Search bar focus | Expands slightly, shows shadow, recent searches dropdown |
| Card hover | Elevates with shadow, image subtle zoom |
| Price change | Animate number transition when dates change |
| Booking confirm | Confetti-like checkmark animation |
| AI chat typing | Three-dot bounce indicator |
| Add to cart | Slide-in notification from top |
| Scroll | Header shrinks, sticky search bar appears |
