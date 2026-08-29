# Afraz Studio App — Product & Technical Reference

## 1. Project Overview

**Product:** Persian mobile application for **آتلیه افراز قم (Afraz Studio Qom)**  
**Primary Platform:** iPhone-first mobile app delivered through a **WebView shell**  
**Language:** Persian (Farsi)  
**Direction:** RTL (Right-to-Left)  
**Primary Design Target:** iPhone 17 Pro Max  
**Frontend:** Vue 3 + TypeScript  
**Backend:** ASP.NET Core on .NET 10  
**Database:** SQL Server

The product is intended for a professional baby, newborn, child, pregnancy, family, birthday, and outdoor photography studio.

The app must cover the full customer lifecycle:

1. Discover studio work
2. Book a photography session
3. Pay reservation fees
4. View previous photography orders
5. View private customer galleries
6. Select photos for print
7. Configure print sizes and quantities
8. Pay print orders
9. Build and order personalized physical albums
10. Buy ready-made albums and photography products
11. Pay any remaining balance
12. Track reservations, orders, albums, payments, and notifications

---

# 2. Core Business Requirements

## 2.1 Authentication

The application must support:

- User registration
- Login
- OTP login
- Mobile number verification
- Logout
- Refresh token
- Forgot/reset password if password-based login is enabled
- Customer profile
- Secure session persistence inside WebView

Recommended authentication model:

- JWT access token
- Refresh token
- ASP.NET Core Identity or custom identity implementation

---

## 2.2 Customer Profile

Each customer should be able to manage:

- First name
- Last name
- Mobile number
- Profile image
- Addresses
- Previous reservations
- Previous photography orders
- Payments
- Favorites
- Notifications
- Support requests

Optional useful fields:

- Child names
- Child birth dates
- Preferred photography styles

---

# 3. Home Page

The Home page is the primary discovery and action screen.

## Required Sections

### Header

- Afraz Studio logo
- Studio title: **آتلیه افراز قم**
- Notifications
- Search
- iPhone Dynamic Island area respected
- RTL layout

### Story-like Photography Categories

Instagram-inspired circular shortcuts may be used for:

- نوزاد
- کودک
- بارداری
- تولد
- خانوادگی
- فضای باز
- ثبت لحظه‌ها / رزرو

These items may navigate to filtered public portfolios.

### Hero Banner

Primary message example:

> ثبت لحظه‌های شیرین کودکی  
> خاطره‌هایی که ماندگار می‌شوند...

Primary CTA:

> رزرو نوبت عکاسی

Hero/banner images should use premium photography.

### Quick Actions

Required quick actions:

- رزرو نوبت
- سفارش‌های من
- انتخاب عکس برای چاپ
- ساخت آلبوم

### Featured Portfolio

Title:

> نمونه‌کارهای منتخب

Requirements:

- Featured photos selected by admin
- Horizontal scrolling
- Publicly visible
- Link to full portfolio
- Images are the visual focus

### Photography Categories

After Featured Portfolio:

- نوزاد
- کودک
- تولد
- بارداری
- خانوادگی
- فضای باز

### Customer Context

If relevant, show actionable customer state such as:

- Photos ready for selection
- Latest active order
- Upcoming booking
- Outstanding payment
- Album ready for pickup

Example:

> عکس‌های شما آماده انتخاب است  
> 235 عکس جدید  
> مشاهده و انتخاب عکس‌ها

### Album Store Preview

Show a compact preview of physical albums/products.

### Studio Information

Further down the scroll:

- Address
- Phone
- Instagram
- Eitaa
- Working hours

### Legal & Trust Information

Show:

- Enamad
- Studio license
- Other required legal certificates

---

# 4. Photography Portfolio

The app must provide a public portfolio.

## Features

- Featured works
- Category filtering
- Horizontal or grid browsing
- Fullscreen image viewer
- Category-specific gallery
- Admin-controlled featured items

## Categories

- Newborn
- Child
- Birthday
- Pregnancy
- Family
- Outdoor

---

# 5. Photography Booking

The booking flow must be complete and transactional.

## Booking Flow

1. Select photography service
2. Select package
3. Select date
4. Select available time
5. Enter customer/session information
6. Review booking
7. Pay reservation fee or full amount
8. Receive confirmation

---

## 5.1 Photography Services

Examples:

- عکاسی نوزاد
- عکاسی کودک
- عکاسی تولد
- عکاسی بارداری
- عکاسی خانوادگی
- عکاسی فضای باز

Each service can have:

- Name
- Description
- Cover image
- Active/inactive status
- Available packages

---

## 5.2 Photography Packages

Each package can include:

- Name
- Photography service
- Duration
- Number of captured photos
- Number of edited photos
- Number of decor/setups
- Base price
- Reservation/deposit amount
- Description
- Active status

---

## 5.3 Date Selection

Requirements:

- Persian/Jalali calendar
- Available days
- Unavailable days
- Fully booked days
- Selected day
- Today
- Studio holidays

Availability must be backend-controlled.

---

## 5.4 Time Selection

For a selected day, return available time slots.

Example:

- 09:00
- 10:30
- 12:00
- 14:00
- 16:00
- 18:00

Booking concurrency must be handled on the backend to prevent double booking.

---

## 5.5 Booking Information

Possible fields:

- Parent/customer name
- Child name
- Child age or birth date
- Mobile number
- Notes
- Special requirements
- Selected service
- Selected package
- Selected date
- Selected time

---

## 5.6 Booking Payment

Supported options:

- Reservation/deposit payment
- Full payment

Store:

- Total price
- Deposit
- Paid amount
- Remaining balance
- Payment status

---

# 6. Reservations

Customers must be able to view:

- Upcoming reservations
- Completed reservations
- Cancelled reservations

## Reservation Details

Include:

- Reservation number
- Service
- Package
- Date
- Time
- Status
- Total amount
- Paid amount
- Remaining amount
- Studio location
- Notes/instructions

Potential actions:

- Cancel
- Reschedule
- Pay remaining amount
- Open related photography order

Cancellation and rescheduling rules must be configurable.

---

# 7. Photography Orders

A photography order represents the post-session customer workflow.

Each order may contain:

- Order number
- Reservation reference
- Session type
- Session date
- Order status
- Gallery status
- Number of photos
- Print order status
- Album status
- Payment status

Possible statuses:

- Processing
- Editing
- Photos ready
- Customer selection pending
- Print processing
- Ready for delivery
- Delivered
- Completed

---

# 8. Private Customer Gallery

This is a core feature.

Customers must only see galleries belonging to their own orders.

## Features

- View session photos
- Fullscreen viewer
- Swipe between photos
- Zoom
- Favorite
- Multi-select
- Selection count
- Filter selected/favorites
- Load optimized image thumbnails
- Fetch high-resolution versions only when needed

Security:

- Gallery authorization must be enforced by backend
- Never trust customer-supplied order IDs without ownership checks
- Use signed or protected asset URLs where appropriate

---

# 9. Photo Printing

Customers can select photos from previous orders for physical printing.

## Flow

1. Open photography order
2. Open gallery
3. Select multiple photos
4. Configure print size
5. Configure quantity
6. Review print cart
7. Pay
8. Track print order

## Print Sizes

Examples:

- 10×15
- 13×18
- 15×21
- 20×30
- 30×40

Print sizes and pricing must be admin-configurable.

Each selected photo may have:

- Photo ID
- Print size
- Quantity
- Unit price
- Line total

---

# 10. Print Orders

A print order should contain:

- Customer
- Related photography order
- Selected photo items
- Print sizes
- Quantities
- Total amount
- Payment
- Status
- Delivery method

Statuses may include:

- Draft
- Awaiting payment
- Paid
- In production
- Ready for pickup
- Shipped
- Delivered
- Cancelled

---

# 11. Personalized Album Builder

Customers must be able to create and order a custom physical album.

## Flow

1. Start album creation
2. Select album type
3. Select album size
4. Select cover type
5. Select cover material
6. Select cover color
7. Select page count
8. Select photos
9. Personalize album
10. Preview
11. Review price
12. Pay
13. Track production

---

## 11.1 Album Options

### Album Type

Examples:

- آلبوم نوزاد
- آلبوم کودک
- آلبوم خانوادگی
- آلبوم لوکس

### Sizes

Examples:

- 20×20
- 25×25
- 30×30

### Cover Materials

Examples:

- Leather
- Fabric
- Printed
- Wood

### Page Counts

Examples:

- 20
- 30
- 40
- 50

All options and prices must be configurable by admin.

---

## 11.2 Album Personalization

Possible customization:

- Child name
- Album title
- Date
- Cover text
- Cover color
- Font choice
- Design style

---

# 12. Online Store

The app must include an online store for ready-made photography products.

## Product Types

Examples:

- Baby albums
- Newborn albums
- Family albums
- Leather albums
- Photo frames
- Photo boxes
- Premium print products

## Store Features

- Product listing
- Categories
- Search
- Product details
- Product images
- Variants/options
- Quantity
- Add to cart
- Favorites
- Checkout
- Delivery method
- Online payment

---

# 13. Shopping Cart

Cart must support:

- Product
- Variant
- Quantity
- Unit price
- Discount
- Subtotal
- Shipping
- Total

Actions:

- Increase quantity
- Decrease quantity
- Remove item
- Apply coupon if supported
- Continue checkout

---

# 14. Checkout & Delivery

Delivery options:

- Studio pickup
- Shipping

## Address

Possible fields:

- Recipient name
- Mobile
- Province
- City
- Address
- Postal code

---

# 15. Payments

The application must support online payment for:

- Reservation deposit
- Full reservation amount
- Print orders
- Personalized album orders
- Store orders
- Remaining balance

## Payment Data

Store:

- Payment ID
- Customer
- Related entity type
- Related entity ID
- Amount
- Gateway
- Gateway tracking code
- Internal reference
- Status
- Created date
- Paid date
- Failure reason

Statuses:

- Pending
- Processing
- Successful
- Failed
- Cancelled
- Refunded

Payment callbacks must be validated on the backend.

---

# 16. Remaining Balance

Customers must be able to pay remaining photography balance after or around physical delivery.

Show:

- Order
- Total price
- Previously paid
- Remaining amount
- Delivery status

CTA:

> پرداخت مانده حساب

---

# 17. Notifications

Notification examples:

- Reservation confirmed
- Appointment reminder
- Photos ready for selection
- Print order ready
- Album ready
- Payment successful
- Payment failed
- Remaining balance reminder

Possible channels:

- In-app
- Push notification
- SMS where needed

---

# 18. Favorites

Customers may favorite:

- Public portfolio items
- Store products
- Private gallery photos

Private gallery favorites must remain scoped to the relevant customer.

---

# 19. Search

Search may cover:

- Portfolio
- Store products
- Albums

Private customer assets should not be globally searchable.

---

# 20. Studio Information

Required public information:

- Studio name
- Description
- Address
- Phone
- Instagram
- Eitaa
- Working hours
- Map/location
- Licenses
- Enamad
- Terms and policies

---

# 21. Support

The app should support:

- FAQ
- Contact information
- Support ticket
- Ticket details
- Ticket status

---

# 22. Legal Pages

Required pages:

- Terms and conditions
- Privacy policy
- Booking policy
- Cancellation policy
- Printing policy
- Delivery policy

---

# 23. Admin Requirements

Although the customer app is the initial focus, the backend must support administrative management for:

## Portfolio

- Add/remove portfolio image
- Assign category
- Mark/unmark as featured
- Sort featured portfolio
- Publish/unpublish

## Booking

- Manage services
- Manage packages
- Define availability
- Define closed days
- Define time slots
- View reservations
- Cancel/reschedule reservation

## Orders

- Manage order status
- Upload photography gallery
- Mark photos ready
- Mark print order ready
- Mark album ready
- Mark delivered

## Printing

- Manage print sizes
- Manage prices
- Manage print orders

## Albums

- Manage album types
- Sizes
- Materials
- Colors
- Page counts
- Pricing

## Store

- Product management
- Categories
- Inventory if required
- Product images
- Prices
- Discounts

## Payments

- View transactions
- Reconcile payments
- View failures
- Refund support if business requires it

## Customers

- Customer list
- Customer details
- Orders
- Reservations
- Payments

---

# 24. UX & Design Requirements

## General

- Persian
- RTL
- iPhone-first
- Target layout: iPhone 17 Pro Max
- Mobile-first responsive WebView
- Modern and minimal
- Photography-first
- Friendly for baby/child photography
- Bright, cheerful color system
- Instagram-like visual hierarchy where useful
- Apple iOS 26-inspired navigation
- Liquid Glass bottom navigation
- Persian sans-serif font
- Use consistent scale for typography and icons

## Home Navigation

Bottom tabs:

- خانه
- رزرو
- سفارش‌ها
- فروشگاه
- پروفایل

## UI Guidelines

- Maintain standard section spacing
- Respect iOS safe areas
- Support Dynamic Island top area
- Use `env(safe-area-inset-top)`
- Use `env(safe-area-inset-bottom)`
- Avoid overly dense dashboards
- Prefer photography over decorative UI
- Use horizontal scroll for selected portfolio
- Use custom iconography with consistent stroke/visual scale

---

# 25. Frontend Technology Stack

## Core

- Vue 3
- TypeScript
- Vite

## State / Navigation / HTTP

- Pinia
- Vue Router
- Axios

## UI

- Tailwind CSS
- Custom component library
- Headless UI / Radix Vue where useful
- Lucide or Material Symbols as icon base
- Custom SVG icons for branded features

## Forms & Validation

- VeeValidate
- Zod

## Animation

- Motion Vue

## Mobile Shell

- Capacitor

## Date

- Persian/Jalali date support
- Day.js if required for general date utilities

## Other

- CSS safe-area support
- Mobile-first layout
- WebView-aware navigation
- Camera/file-picker integration where required
- Push notification integration through Capacitor/native layer

---

# 26. Backend Technology Stack

## Platform

- .NET 10
- ASP.NET Core Web API

## Architecture

Use:

- Clean Architecture
- Vertical Slice Architecture
- CQRS

The project should remain feature-oriented instead of being organized only by technical layers.

## Recommended Libraries

- MediatR
- FluentValidation
- EF Core 10
- SQL Server
- ASP.NET Core Identity or custom authentication
- Serilog
- OpenAPI / Swagger
- ProblemDetails

## Infrastructure

- Redis
- S3-compatible object storage for photography files
- CDN for image delivery
- Hangfire or Quartz for background jobs
- Firebase Cloud Messaging
- Docker
- Docker Compose

---

# 27. Backend Architecture

Recommended solution structure:

```text
src/
├── Afraz.Api/
├── Afraz.Application/
├── Afraz.Domain/
├── Afraz.Infrastructure/
└── Afraz.Features/
```

An alternative is to place Vertical Slices directly inside Application or Api if that produces less accidental complexity.

Prefer pragmatic Clean Architecture.

Do not create abstractions without a real reason.

---

# 28. Vertical Slice Structure

Example:

```text
Features/
└── Bookings/
    ├── CreateBooking/
    │   ├── Command.cs
    │   ├── Handler.cs
    │   ├── Validator.cs
    │   ├── Response.cs
    │   └── Endpoint.cs
    │
    ├── GetBooking/
    │   ├── Query.cs
    │   ├── Handler.cs
    │   ├── Response.cs
    │   └── Endpoint.cs
    │
    ├── CancelBooking/
    └── GetAvailableSlots/
```

Other feature areas:

```text
Features/
├── Authentication/
├── Customers/
├── Portfolio/
├── PhotographyServices/
├── PhotographyPackages/
├── Bookings/
├── Orders/
├── Galleries/
├── Prints/
├── PrintOrders/
├── AlbumBuilder/
├── AlbumOrders/
├── Store/
├── Products/
├── Cart/
├── Checkout/
├── Payments/
├── Notifications/
├── Favorites/
├── Support/
└── Studio/
```

---

# 29. CQRS Rules

Commands modify state.

Examples:

- CreateBookingCommand
- CancelBookingCommand
- SelectGalleryPhotosCommand
- CreatePrintOrderCommand
- CreateAlbumOrderCommand
- CreateStoreOrderCommand
- ConfirmPaymentCommand

Queries return data.

Examples:

- GetHomeQuery
- GetPortfolioQuery
- GetAvailableBookingDatesQuery
- GetAvailableTimeSlotsQuery
- GetCustomerOrdersQuery
- GetOrderGalleryQuery
- GetAlbumOptionsQuery
- GetStoreProductsQuery

Do not implement CQRS as two databases unless a real scaling requirement appears.

Use CQRS primarily for clear application behavior separation.

---

# 30. EF Core

Use:

- EF Core 10
- SQL Server
- Fluent configuration
- Migrations
- Optimistic concurrency where useful
- Transactions for business-critical workflows

Avoid generic repositories around EF Core unless there is a demonstrated need.

`DbContext` already acts as Unit of Work / Repository abstraction.

---

# 31. Suggested Domain Entities

Possible initial entities:

```text
Customer
CustomerAddress
RefreshToken

PhotographyService
PhotographyPackage

Booking
BookingTimeSlot
StudioClosedDate

PhotographyOrder
PhotographyOrderStatusHistory

Gallery
GalleryPhoto
PhotoFavorite

PrintSize
PrintOrder
PrintOrderItem

AlbumType
AlbumSize
AlbumMaterial
AlbumColor
AlbumOption
AlbumOrder
AlbumOrderPhoto

ProductCategory
Product
ProductImage
ProductVariant

Cart
CartItem

StoreOrder
StoreOrderItem

Payment
PaymentTransaction

Notification

PortfolioCategory
PortfolioItem

SupportTicket
SupportTicketMessage

StudioInformation
LegalDocument
```

This is an initial model and should be refined during implementation.

---

# 32. Storage Strategy

Do NOT store original photography files inside SQL Server.

Use object storage.

Recommended:

```text
SQL Server
    |
    └── metadata / references

Object Storage
    |
    ├── gallery originals
    ├── optimized images
    ├── thumbnails
    ├── portfolio
    ├── album previews
    └── product images
```

Use CDN in front of storage where possible.

Potential image variants:

- thumbnail
- medium
- high resolution
- original

The customer UI should primarily use optimized images.

---

# 33. Redis Usage

Potential Redis use cases:

- Cache public portfolio
- Cache studio configuration
- Cache album options
- Cache store categories
- Distributed locks for booking
- Temporary checkout state
- Rate limiting

Do not use Redis as the system of record.

---

# 34. Background Jobs

Use Hangfire or Quartz for:

- Appointment reminders
- Notification delivery
- Image processing
- Thumbnail generation
- Payment reconciliation
- Cleanup jobs
- Expired reservation handling
- Abandoned cart tasks if needed

---

# 35. API Guidelines

Use RESTful endpoints with feature-oriented paths.

Examples:

```text
POST   /api/auth/login
POST   /api/auth/otp/request
POST   /api/auth/otp/verify

GET    /api/home

GET    /api/portfolio
GET    /api/portfolio/categories/{id}

GET    /api/booking/services
GET    /api/booking/packages
GET    /api/booking/availability
POST   /api/bookings

GET    /api/bookings/me
GET    /api/bookings/{id}

GET    /api/orders/me
GET    /api/orders/{id}
GET    /api/orders/{id}/gallery

POST   /api/print-orders
POST   /api/album-orders

GET    /api/store/products
GET    /api/store/products/{id}

POST   /api/payments
POST   /api/payments/callback
```

Use consistent error contracts.

---

# 36. API Error Contract

Use RFC-compatible ProblemDetails.

Example:

```json
{
  "type": "https://api.afrazstudio.ir/errors/booking-slot-unavailable",
  "title": "Booking time is no longer available",
  "status": 409,
  "detail": "The selected 14:00 time slot has already been reserved.",
  "traceId": "..."
}
```

---

# 37. Validation

Use FluentValidation.

Validation must exist server-side even if frontend already validates.

Examples:

- Valid mobile number
- Booking date cannot be in the past
- Selected time slot must be available
- Selected photos must belong to customer
- Print size must be active
- Payment amount must match backend calculation
- Product must be available
- Album configuration must be valid

Never trust price values sent by the client.

---

# 38. Security

Required principles:

- HTTPS only
- JWT authentication
- Refresh token rotation
- Authorization checks per customer
- Rate limiting
- Input validation
- Secure payment callback verification
- Secure object storage
- Avoid exposing original files publicly
- Audit important payment/order state changes
- Never trust frontend totals
- Never trust frontend ownership claims

---

# 39. Important Business Invariants

These rules should be enforced in backend code.

## Booking

- A time slot cannot be double-booked.
- Booking prices are calculated by the backend.
- Only active service/package combinations are valid.
- Payment does not confirm booking until gateway verification succeeds if payment is required.

## Gallery

- A customer can only access their own gallery.
- Only photos from valid customer galleries can be selected for print/album.

## Printing

- Print price must be resolved from active backend pricing.
- Quantity must be positive.
- Print size must be valid.

## Albums

- Album options must form a valid configuration.
- Album price must be calculated by backend.
- Selected photos must belong to the customer.

## Store

- Product price must be backend-controlled.
- Inventory must be validated before final order if inventory tracking is enabled.

## Payments

- Every successful payment must be idempotently verified.
- Duplicate gateway callbacks must not create duplicate business effects.

---

# 40. Observability

Recommended:

- Serilog
- Structured logging
- Request correlation ID
- Payment audit logs
- Booking status logs
- Order status history
- Health checks
- Metrics if production scale requires them

---

# 41. Testing

## Backend

- xUnit
- FluentAssertions
- NSubstitute or Moq
- Testcontainers
- Integration tests against SQL Server container where practical

Focus tests on:

- Booking availability
- Double-book prevention
- Authorization
- Payment idempotency
- Price calculation
- Photo ownership
- Album pricing
- Print order pricing

## Frontend

- Vitest
- Vue Test Utils
- Playwright for critical end-to-end flows

Critical E2E flows:

1. Login
2. Book session
3. Pay reservation
4. Open gallery
5. Select photos
6. Create print order
7. Build album
8. Store checkout

---

# 42. Coding Guidelines for Codex

Codex should follow these rules while implementing the project:

1. Prefer simple, readable code.
2. Do not introduce unnecessary abstractions.
3. Use Vertical Slices for application use cases.
4. Keep domain rules close to the relevant feature/domain.
5. Do not create a generic repository over EF Core by default.
6. Use async APIs throughout I/O paths.
7. Support CancellationToken.
8. Keep endpoints thin.
9. Validation belongs in validators and domain/application rules.
10. Never trust frontend price calculations.
11. Protect every customer-owned resource with authorization checks.
12. Use idempotency in payments.
13. Add tests for important business invariants.
14. Use database constraints where they provide additional safety.
15. Keep frontend mobile-first and RTL-first.
16. Keep design system reusable instead of duplicating styles.
17. All user-visible frontend content must be Persian unless explicitly required otherwise.
18. Respect iOS safe areas inside WebView.
19. Keep the UI photography-first.
20. Follow existing project conventions once the repository has been initialized.

---

# 43. Suggested Implementation Order

## Phase 1 — Foundation

- Repository structure
- Docker Compose
- SQL Server
- Redis
- Backend skeleton
- Vue skeleton
- Authentication
- Design system
- Logging
- Error handling

## Phase 2 — Public Experience

- Home
- Portfolio
- Categories
- Studio information

## Phase 3 — Booking

- Services
- Packages
- Availability
- Booking
- Reservation payment
- Reservation management

## Phase 4 — Customer Orders

- Orders
- Private galleries
- Favorites
- Photo viewer

## Phase 5 — Printing

- Photo selection
- Print configuration
- Print cart
- Print payment
- Order tracking

## Phase 6 — Album Builder

- Album options
- Photo selection
- Customization
- Preview
- Pricing
- Payment
- Tracking

## Phase 7 — Store

- Products
- Categories
- Product details
- Cart
- Checkout
- Delivery
- Payment

## Phase 8 — Notifications & Support

- Notifications
- Push
- FAQ
- Support tickets

## Phase 9 — Admin

- Portfolio management
- Booking configuration
- Orders
- Gallery upload
- Print management
- Album management
- Store management
- Payments

---

# 44. Codex Starting Instruction

Use this document as the primary product and architecture reference.

When starting implementation:

1. Analyze this specification.
2. Propose the initial repository/solution structure.
3. Do not implement all features at once.
4. Start with the project foundation.
5. Create a short implementation plan.
6. Implement one vertical slice at a time.
7. Add tests for business-critical behavior.
8. Keep a `docs/` directory for architecture and business decisions.
9. Update documentation when implementation decisions materially change this reference.
10. Do not silently remove or simplify business requirements.

Suggested first Codex task:

```text
Read docs/afraz-studio-reference.md and initialize the project foundation.

Create:

- ASP.NET Core .NET 10 backend
- Vue 3 + TypeScript + Vite frontend
- Clean Architecture with pragmatic Vertical Slice + CQRS
- EF Core 10 + SQL Server
- Redis integration
- Docker Compose
- Serilog
- ProblemDetails
- FluentValidation
- MediatR
- JWT/Refresh Token authentication skeleton
- Initial Vue design system and RTL setup
- Tailwind CSS
- Pinia
- Vue Router
- Axios
- Capacitor skeleton

Do not implement business features yet.

Create a concise README describing how to run the project locally.
```

---

# 45. Final Architecture Summary

```text
iOS / Android
    │
    ▼
Capacitor Native Shell
    │
    ▼
Vue 3 WebView Application
    │
    ▼
ASP.NET Core .NET 10 API
    │
    ├── SQL Server
    ├── Redis
    ├── Object Storage
    ├── Payment Gateway
    └── Firebase Push Notifications
```

Backend architecture:

```text
Clean Architecture
+
Vertical Slice Architecture
+
CQRS
+
EF Core
```

The implementation should prioritize:

**Business correctness > simplicity > maintainability > performance optimization.**

Optimize only where real measurements justify additional complexity.
