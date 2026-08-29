# Afraz Studio — Proposed Frontend & Backend Architecture

## 1. Purpose

This document describes the proposed application architecture for **Afraz Studio**.

The goal is to keep the system:

- Simple
- Maintainable
- Business-oriented
- Secure
- Ready to scale
- Easy for Codex and developers to extend feature-by-feature

The project should begin as a **single repository** and a **Modular Monolith**, with both frontend and backend maintained together.

---

# 2. High-Level Architecture

```text
┌──────────────────────────────────────┐
│        iOS / Android Application     │
│                                      │
│         Capacitor WebView Shell      │
└──────────────────┬───────────────────┘
                   │
                   ▼
┌──────────────────────────────────────┐
│        Vue 3 + TypeScript SPA        │
│         RTL / Mobile First           │
└──────────────────┬───────────────────┘
                   │ HTTPS / JSON
                   ▼
┌──────────────────────────────────────┐
│      ASP.NET Core Web API (.NET 10)  │
│                                      │
│   Modular Monolith                   │
│   Clean Architecture                 │
│   Vertical Slice Architecture        │
│   CQRS                               │
└──────────┬───────────┬───────────────┘
           │           │
           ▼           ▼
      SQL Server      Redis
           │
           ▼
    Object Storage / CDN

External Integrations:

- Payment Gateway
- SMS Provider
- Firebase Push Notifications
```

---

# 3. Repository Strategy

Use a **single Git repository** for frontend and backend.

Recommended structure:

```text
afraz-studio/
│
├── src/
│   ├── backend/
│   │   ├── Afraz.Api/
│   │   ├── Afraz.Application/
│   │   ├── Afraz.Domain/
│   │   └── Afraz.Infrastructure/
│   │
│   └── frontend/
│       ├── src/
│       ├── public/
│       ├── package.json
│       ├── vite.config.ts
│       └── ...
│
├── tests/
│   ├── backend/
│   │   ├── Afraz.UnitTests/
│   │   ├── Afraz.IntegrationTests/
│   │   └── Afraz.ArchitectureTests/
│   │
│   └── frontend/
│
├── docs/
│   ├── adr/
│   ├── afraz-studio-reference.md
│   ├── afraz-studio-constitution.md
│   └── architecture.md
│
├── AGENTS.md
├── docker-compose.yml
├── .editorconfig
├── .gitignore
├── README.md
└── AfrazStudio.sln
```

Benefits:

- One pull request can change frontend and backend together.
- Shared business features stay synchronized.
- Easier local development.
- Easier Codex context.
- One CI/CD pipeline.
- One production artifact can be created.

---

# 4. Production Hosting Model

The Vue frontend should be built into ASP.NET Core static assets.

Production flow:

```text
src/frontend
      │
      │ npm run build
      ▼
src/backend/Afraz.Api/wwwroot
      │
      │ dotnet publish
      ▼
Single ASP.NET Core Deployment Artifact
```

ASP.NET Core will serve:

```text
/api/*          → Backend API
/assets/*       → Vue JS/CSS/images
/booking        → Vue index.html
/orders         → Vue index.html
/profile        → Vue index.html
/store          → Vue index.html
```

SPA fallback:

```text
app.UseStaticFiles();

app.Map...API endpoints...

app.MapFallbackToFile("index.html");
```

Important:

`/api/*` must not be swallowed by SPA fallback.

---

# 5. Development Hosting Model

During development, keep frontend and backend separate for fast hot reload.

```text
Vue / Vite
localhost:5173
      │
      │ /api proxy
      ▼
ASP.NET Core
localhost:xxxx
      │
      ├── SQL Server
      └── Redis
```

Example workflow:

```bash
docker compose up -d

dotnet run --project src/backend/Afraz.Api

npm run dev --prefix src/frontend
```

Vite proxies:

```text
/api → ASP.NET Core
```

This gives:

- Fast frontend HMR
- Independent backend debugging
- Single production deployment later

---

# 6. Frontend Architecture

## 6.1 Technology Stack

Use:

- Vue 3
- TypeScript
- Vite
- Composition API
- `<script setup lang="ts">`
- Tailwind CSS
- Pinia
- Vue Router
- Axios
- VeeValidate
- Zod
- Motion Vue
- Capacitor
- Custom SVG icons
- Lucide / Material Symbols as base icons
- Vitest
- Vue Test Utils
- Playwright

---

# 7. Frontend Design Principles

The frontend should be:

- Feature-oriented
- Mobile-first
- RTL-first
- iPhone-first
- Photography-first
- Component-driven
- Strictly typed
- Easy to test

Avoid:

- One giant global store
- One giant `components/` directory
- Random hard-coded styles
- API calls directly inside every page
- Duplicate business logic from backend

---

# 8. Frontend Folder Structure

Recommended:

```text
src/frontend/src/
│
├── app/
│   ├── AppShell.vue
│   ├── app.config.ts
│   └── providers/
│
├── assets/
│   ├── fonts/
│   ├── icons/
│   ├── images/
│   └── patterns/
│
├── components/
│   ├── ui/
│   │   ├── AppButton.vue
│   │   ├── AppIcon.vue
│   │   ├── AppInput.vue
│   │   ├── AppModal.vue
│   │   ├── GlassBottomNav.vue
│   │   └── ...
│   │
│   └── shared/
│
├── features/
│   ├── auth/
│   ├── home/
│   ├── booking/
│   ├── orders/
│   ├── gallery/
│   ├── printing/
│   ├── albums/
│   ├── store/
│   ├── checkout/
│   ├── payments/
│   ├── notifications/
│   └── profile/
│
├── router/
│   └── index.ts
│
├── stores/
│
├── services/
│
├── composables/
│
├── styles/
│   ├── tokens.css
│   ├── typography.css
│   ├── globals.css
│   └── safe-area.css
│
├── types/
│
├── App.vue
└── main.ts
```

---

# 9. Feature Structure

Each frontend feature should own its own logic.

Example:

```text
features/
└── booking/
    ├── api/
    │   └── booking.api.ts
    │
    ├── components/
    │   ├── BookingPackageCard.vue
    │   └── TimeSlotItem.vue
    │
    ├── composables/
    │   └── useBooking.ts
    │
    ├── pages/
    │   ├── BookingServicePage.vue
    │   ├── BookingPackagePage.vue
    │   ├── BookingDatePage.vue
    │   └── BookingReviewPage.vue
    │
    ├── schemas/
    │   └── booking.schema.ts
    │
    └── types/
        └── booking.types.ts
```

Feature-specific code should not be moved into global folders unless it is truly shared.

---

# 10. Frontend State Management

Use Pinia only for shared application-level state.

Good Pinia use cases:

- Authenticated customer
- Authentication/session state
- Cart
- Notification counters
- Global application settings

Prefer local state for:

- Form input
- Open modal
- Selected gallery image
- UI tab state
- Temporary page state

Do not put all server data into Pinia.

---

# 11. Frontend API Layer

Use Axios through typed API clients.

Example:

```text
features/booking/api/booking.api.ts
features/gallery/api/gallery.api.ts
features/store/api/store.api.ts
```

Components should call feature services/composables rather than directly constructing raw Axios requests everywhere.

Example:

```ts
export async function getAvailableSlots(date: string) {
    return apiClient.get<AvailableSlot[]>('/api/bookings/availability', {
        params: { date }
    });
}
```

---

# 12. Frontend Validation

Use:

- VeeValidate
- Zod

Frontend validation is primarily for UX.

Backend validation remains authoritative.

Examples:

- Mobile number format
- Required fields
- Address fields
- Print quantity
- Album customization

Do not trust frontend validation for:

- Price
- Availability
- Ownership
- Payment verification

---

# 13. Frontend Routing

Use Vue Router with history mode.

Primary routes may include:

```text
/
 /booking
 /bookings
 /orders
 /orders/:id
 /orders/:id/gallery
 /printing
 /albums
 /store
 /cart
 /checkout
 /payments
 /notifications
 /profile
```

ASP.NET Core must serve `index.html` for these routes in production.

---

# 14. Frontend Mobile Shell

Use Capacitor for the mobile wrapper.

Capacitor provides access to:

- Push notifications
- Camera
- File picker
- App lifecycle
- Deep links
- Native share
- Status bar
- Splash screen
- Native permissions

Do not directly mix native code into normal Vue components.

Use narrow wrapper services/composables.

Example:

```text
services/native/
├── notifications.ts
├── camera.ts
├── deep-links.ts
└── files.ts
```

---

# 15. RTL & Mobile Rules

The app is Persian and RTL.

Configure:

```html
<html lang="fa" dir="rtl">
```

Use CSS logical properties where possible:

```css
padding-inline
margin-inline
inset-inline
border-inline
```

Respect:

```css
env(safe-area-inset-top)
env(safe-area-inset-bottom)
```

Primary device target:

**iPhone 17 Pro Max**

Important mobile concerns:

- Dynamic Island
- Safe areas
- Home indicator
- Keyboard resize
- Fixed bottom navigation clearance
- Touch targets
- Horizontal gallery direction

---

# 16. Frontend Design System

Use reusable tokens.

Example:

```text
styles/
├── tokens.css
├── typography.css
└── globals.css
```

Example token categories:

```text
Color
Typography
Spacing
Radius
Shadow
Z-index
Safe Area
Motion
```

Do not hard-code random visual values inside feature components.

---

# 17. Backend Architecture

Use a **pragmatic Clean Architecture** combined with:

- Modular Monolith
- Vertical Slice Architecture
- CQRS

Do not create unnecessary abstraction layers.

High-level dependency direction:

```text
Afraz.Domain
     ↑
Afraz.Application
     ↑
Afraz.Infrastructure
     ↑
Afraz.Api
```

`Afraz.Api` is the composition root.

---

# 18. Backend Projects

## Afraz.Domain

Contains:

- Core domain entities
- Value objects
- Domain rules
- Domain enums
- Domain events where useful

Must not depend on Infrastructure or API.

---

## Afraz.Application

Contains:

- Vertical slices
- Commands
- Queries
- Handlers
- Validators
- DTOs / response models
- Application interfaces
- Application policies

Application should depend on Domain.

---

## Afraz.Infrastructure

Contains:

- EF Core
- SQL Server
- Redis
- Object storage
- Payment gateway adapters
- SMS adapters
- Firebase integration
- Background job infrastructure

Implements interfaces owned by Application where appropriate.

---

## Afraz.Api

Contains:

- Application host
- Dependency injection composition
- Endpoints
- Authentication setup
- Middleware
- ProblemDetails
- OpenAPI
- Static Vue hosting
- SPA fallback

Avoid business logic here.

---

# 19. Vertical Slice Architecture

Organize behavior by feature/use case.

Example:

```text
Afraz.Application/
└── Features/
    └── Bookings/
        ├── CreateBooking/
        │   ├── Command.cs
        │   ├── Handler.cs
        │   ├── Validator.cs
        │   └── Response.cs
        │
        ├── GetBooking/
        ├── GetAvailableSlots/
        ├── CancelBooking/
        └── RescheduleBooking/
```

Avoid large technical folders such as:

```text
Services/
Repositories/
Managers/
Helpers/
```

for unrelated business behavior.

---

# 20. CQRS

Use CQRS to separate write behavior from read behavior.

Commands:

```text
CreateBookingCommand
CancelBookingCommand
CreatePrintOrderCommand
CreateAlbumOrderCommand
CreateStoreOrderCommand
ConfirmPaymentCommand
```

Queries:

```text
GetHomeQuery
GetFeaturedPortfolioQuery
GetAvailableSlotsQuery
GetCustomerOrdersQuery
GetOrderGalleryQuery
GetStoreProductsQuery
```

CQRS here does NOT mean separate databases.

Initially:

```text
Commands ─┐
          ├── EF Core → SQL Server
Queries  ─┘
```

---

# 21. Backend Request Flow

Typical command flow:

```text
HTTP Request
     │
     ▼
ASP.NET Endpoint
     │
     ▼
MediatR Command
     │
     ▼
Validation Pipeline
     │
     ▼
Handler
     │
     ├── Domain Rules
     ├── EF Core
     └── External Integration
     │
     ▼
Response
```

Query flow:

```text
HTTP Request
     │
     ▼
Endpoint
     │
     ▼
MediatR Query
     │
     ▼
Query Handler
     │
     ▼
EF Core Projection
     │
     ▼
Response DTO
```

---

# 22. EF Core Architecture

Use:

- EF Core 10
- SQL Server
- Fluent configurations
- Migrations
- Proper indexes
- Foreign keys
- Unique constraints
- Transactions
- Optimistic concurrency where useful
- Async operations
- CancellationToken

Avoid generic repositories.

Do not create:

```text
IGenericRepository<T>
GenericRepository<T>
```

unless a future concrete need justifies them.

---

# 23. Database Architecture

SQL Server stores transactional and business metadata.

Examples:

```text
Customers
Bookings
PhotographyServices
PhotographyPackages
PhotographyOrders
Galleries
GalleryPhotos
PrintOrders
AlbumOrders
Products
StoreOrders
Payments
Notifications
SupportTickets
```

Photography binaries should not be stored in SQL Server.

---

# 24. File & Image Architecture

Use object storage.

```text
SQL Server
    │
    └── File metadata / Object keys

Object Storage
    │
    ├── originals
    ├── thumbnails
    ├── gallery-medium
    ├── portfolio
    ├── products
    └── album-previews
          │
          ▼
         CDN
          │
          ▼
       Vue App
```

For private galleries, use secured/signed asset delivery where appropriate.

---

# 25. Redis Architecture

Use Redis selectively.

Potential uses:

- Public portfolio cache
- Studio settings cache
- Store/category cache
- Album option cache
- Rate limiting
- Booking coordination
- Temporary workflow state

SQL Server remains the system of record.

---

# 26. Payment Architecture

All payment operations are backend-authoritative.

Flow:

```text
Vue
 │
 │ Create payment request
 ▼
ASP.NET Core
 │
 │ Calculate/validate amount
 ▼
Payment Gateway
 │
 │ Redirect / Gateway UI
 ▼
Gateway Callback
 │
 ▼
ASP.NET Core
 │
 ├── Verify callback
 ├── Check idempotency
 ├── Persist transaction
 └── Update business entity
 │
 ▼
Vue Result Page
```

Never trust a frontend redirect as proof of successful payment.

---

# 27. Booking Concurrency Architecture

Booking must be concurrency-safe.

Frontend availability check:

```text
Customer → GET available slots
```

is not enough.

Final booking must be protected using database-level guarantees.

Recommended:

- Unique slot constraint where applicable
- Transaction
- Recheck availability during command
- Optional temporary reservation/hold model

Database is the final safety boundary.

---

# 28. Authentication Architecture

Recommended:

```text
Login / OTP
     │
     ▼
ASP.NET Core
     │
     ├── Access Token
     └── Refresh Token
```

Use:

- JWT access token
- Refresh token rotation
- Revocation
- Ownership authorization
- Policies where useful

Capacitor token persistence must be designed securely.

---

# 29. Background Jobs Architecture

Use Hangfire for durable jobs.

Examples:

```text
Appointment Reminder
Photo Processing
Push Notification
SMS Notification
Payment Reconciliation
Expired Booking Cleanup
```

Architecture:

```text
ASP.NET Core
     │
     ├── Enqueue Job
     ▼
Hangfire
     │
     ▼
Worker
     │
     ├── SQL Server
     ├── Object Storage
     ├── SMS
     └── Firebase
```

---

# 30. Error Architecture

Use centralized ASP.NET Core exception handling.

Return ProblemDetails.

Example:

```json
{
  "type": "https://api.afrazstudio.ir/errors/booking-slot-unavailable",
  "title": "Booking time is no longer available",
  "status": 409,
  "detail": "The selected time slot has already been reserved.",
  "traceId": "..."
}
```

Frontend maps these to localized Persian error messages.

---

# 31. Logging & Observability

Use Serilog.

Recommended:

```text
Request
   │
   ├── Correlation ID
   ├── Structured Logs
   └── Trace Information
```

Track important transitions:

- Booking
- Payment
- Photography order
- Print order
- Album order
- Store order

Never log secrets or tokens.

---

# 32. Testing Architecture

Backend:

```text
Unit Tests
   └── Business/domain behavior

Integration Tests
   ├── EF Core
   ├── SQL Server
   ├── API
   ├── Authorization
   └── Payment idempotency
```

Use:

- xUnit
- FluentAssertions
- Testcontainers

Frontend:

```text
Vitest
Vue Test Utils
Playwright
```

Use Playwright for business-critical end-to-end flows.

---

# 33. Main Business Modules

Recommended module boundaries:

```text
Authentication
Customers
Portfolio
Studio
Photography
Bookings
Orders
Galleries
Printing
Albums
Store
Cart
Checkout
Payments
Notifications
Support
Administration
```

Do not extract these modules into microservices initially.

---

# 34. Example Booking End-to-End Architecture

```text
Vue Booking Page
       │
       ▼
booking.api.ts
       │
       ▼
POST /api/bookings
       │
       ▼
CreateBookingCommand
       │
       ▼
CreateBookingHandler
       │
       ├── Validate Service
       ├── Validate Package
       ├── Validate Slot
       ├── Calculate Price
       ├── Enforce Concurrency
       └── Save Booking
       │
       ▼
SQL Server
       │
       ▼
Booking Response
       │
       ▼
Vue Review / Payment
```

---

# 35. Example Gallery Architecture

```text
Vue Gallery
     │
     ▼
GET /api/orders/{id}/gallery
     │
     ▼
GetOrderGalleryQuery
     │
     ├── Validate authenticated customer
     ├── Verify order ownership
     └── Load gallery metadata
     │
     ▼
SQL Server
     │
     └── Object keys
             │
             ▼
       Signed/CDN URLs
             │
             ▼
          Vue Gallery
```

---

# 36. Example Print Order Architecture

```text
Customer selects photos
        │
        ▼
Vue Print Configuration
        │
        ▼
POST /api/print-orders
        │
        ▼
CreatePrintOrderCommand
        │
        ├── Verify photo ownership
        ├── Verify print size
        ├── Resolve backend price
        ├── Calculate total
        └── Persist order
        │
        ▼
SQL Server
        │
        ▼
Payment Flow
```

---

# 37. Example Album Builder Architecture

```text
Vue Album Builder
      │
      ├── Type
      ├── Size
      ├── Material
      ├── Pages
      └── Selected Photos
      │
      ▼
POST /api/album-orders
      │
      ▼
CreateAlbumOrderCommand
      │
      ├── Verify configuration
      ├── Verify photo ownership
      ├── Calculate backend price
      └── Persist
      │
      ▼
SQL Server
      │
      ▼
Payment
```

---

# 38. Build Architecture

Development:

```text
src/frontend
     │
     ├── Vite :5173
     │       │
     │       └── /api proxy
     │
     ▼
ASP.NET Core
```

Production:

```text
npm run build
     │
     ▼
Afraz.Api/wwwroot
     │
     ▼
dotnet publish
     │
     ▼
Single Deployable Application
```

---

# 39. CI/CD Architecture

Recommended:

```text
Pull Request
    │
    ├── dotnet restore
    ├── dotnet build
    ├── dotnet test
    ├── npm ci
    ├── npm run type-check
    ├── npm run lint
    ├── npm run test
    └── npm run build

Merge
    │
    ▼
Build Release Artifact
    │
    ├── Vue → wwwroot
    └── dotnet publish
    │
    ▼
Deploy
    │
    ├── Database migration
    └── Health check
```

---

# 40. Why Modular Monolith

For the initial product, Modular Monolith provides:

- Faster development
- Easier transactions
- Easier local debugging
- One deployment
- Lower operational complexity
- Clear business boundaries
- Easier refactoring
- Easier Codex navigation

The system can still scale horizontally because ASP.NET Core should remain stateless where possible.

---

# 41. When to Consider Service Extraction

Only consider extracting a module when there is a real need.

Examples:

- Independent scaling
- Very heavy image-processing workload
- Team ownership separation
- Operational isolation
- Independent deployment requirement
- Different reliability requirements

Do not extract services because the module has its own folder.

---

# 42. Architecture Anti-Patterns to Avoid

Avoid:

- Generic repository everywhere
- Service classes with hundreds of methods
- Fat controllers
- Business logic in Vue
- Database entities returned directly from API
- One giant Pinia store
- Direct Axios calls everywhere
- Unbounded gallery responses
- Original image downloads in gallery grids
- Frontend-controlled pricing
- Public private-gallery URLs
- Double-booking race conditions
- Premature microservices
- Premature message brokers

---

# 43. Final Architecture Summary

```text
REPOSITORY
Single Monorepo

MOBILE
Capacitor WebView

FRONTEND
Vue 3
TypeScript
Vite
Tailwind CSS
Pinia
Vue Router
Axios
VeeValidate
Zod
Motion Vue

BACKEND
.NET 10
ASP.NET Core
Clean Architecture
Modular Monolith
Vertical Slice Architecture
CQRS
MediatR
FluentValidation

DATA
EF Core 10
SQL Server

INFRASTRUCTURE
Redis
Object Storage
CDN
Hangfire
Firebase
SMS
Payment Gateway

TESTING
xUnit
FluentAssertions
Testcontainers
Vitest
Vue Test Utils
Playwright

PRODUCTION
Vue build → ASP.NET Core wwwroot
dotnet publish → Single deployment artifact
```

---

# 44. Architectural Principle

The preferred architecture is:

> **A simple, well-structured Modular Monolith with strong feature boundaries and backend-controlled business rules.**

Add complexity only when production requirements justify it.
