# Afraz Studio — Engineering Constitution

> **Purpose:** This document is the engineering constitution and technical source of truth for the Afraz Studio application.  
> Codex and all contributors MUST follow these rules unless an explicit Architecture Decision Record (ADR) changes them.

---

## 1. Product Context

**Product:** آتلیه افراز قم — Afraz Studio  
**Application:** Persian mobile-first photography studio application  
**Language:** Persian / Farsi  
**Direction:** RTL  
**Primary device:** iPhone 17 Pro Max  
**Delivery model:** Vue application inside a Capacitor/WebView mobile shell, while keeping the frontend web-compatible.

Core business domains:

- Authentication & Customers
- Portfolio
- Photography Services & Packages
- Booking & Scheduling
- Photography Orders
- Private Galleries
- Photo Selection & Printing
- Personalized Album Builder
- Online Store
- Cart & Checkout
- Payments
- Notifications
- Studio Information
- Support
- Administration

Business requirements are defined separately in:

`docs/afraz-studio-reference.md`

This constitution defines **how the system must be engineered**.

---

# 2. Engineering Principles

The following priorities apply in order:

1. **Business correctness**
2. **Security and data integrity**
3. **Simple and understandable design**
4. **Maintainability**
5. **User experience**
6. **Observability**
7. **Performance**
8. **Scalability when justified by measurements**

Do not introduce distributed-system complexity before it is required.

Prefer a **Modular Monolith** initially.

Do not create microservices merely because domain boundaries exist.

---

# 3. Mandatory Architecture

The backend SHALL use a pragmatic combination of:

- Clean Architecture
- Modular Monolith
- Vertical Slice Architecture
- CQRS
- Domain-oriented modeling
- Feature-based organization

Architecture:

```text
┌──────────────────────────────────────┐
│       Capacitor / Native Shell       │
│           iOS / Android              │
└──────────────────┬───────────────────┘
                   │
                   ▼
┌──────────────────────────────────────┐
│       Vue 3 + TypeScript SPA         │
│      Mobile-first / RTL-first        │
└──────────────────┬───────────────────┘
                   │ HTTPS / JSON
                   ▼
┌──────────────────────────────────────┐
│       ASP.NET Core .NET 10 API       │
│                                      │
│ Clean + Vertical Slice + CQRS        │
│ Modular Monolith                     │
└──────┬────────┬─────────┬────────────┘
       │        │         │
       ▼        ▼         ▼
 SQL Server   Redis   Object Storage
                         │
                         ▼
                        CDN

External integrations:
Payment Gateway
SMS Provider
Firebase Push Notifications
```

---

# 4. Technology Stack

## 4.1 Frontend

Mandatory core stack:

| Area | Technology |
|---|---|
| Framework | Vue 3 |
| Language | TypeScript |
| Build Tool | Vite |
| State | Pinia |
| Routing | Vue Router |
| HTTP | Axios |
| Styling | Tailwind CSS |
| Forms | VeeValidate |
| Schema Validation | Zod |
| Animation | Motion Vue |
| Mobile Shell | Capacitor |
| Icons | Custom SVG + Lucide/Material Symbols as base |
| Unit Tests | Vitest |
| Component Tests | Vue Test Utils |
| E2E Tests | Playwright |

Frontend SHALL use the Vue Composition API and `<script setup lang="ts">`.

Do not introduce Options API for new components.

---

## 4.2 Backend

| Area | Technology |
|---|---|
| Runtime | .NET 10 |
| API | ASP.NET Core Web API |
| ORM | EF Core 10 |
| Database | SQL Server |
| Architecture | Clean + Vertical Slice + CQRS |
| Messaging in-process | MediatR |
| Validation | FluentValidation |
| Authentication | JWT + Refresh Token |
| Identity | ASP.NET Core Identity or justified custom implementation |
| Logging | Serilog |
| API Documentation | OpenAPI |
| Error Contract | ProblemDetails |
| Cache | Redis |
| Background Jobs | Hangfire preferred; Quartz acceptable when justified |
| Testing | xUnit |
| Assertions | FluentAssertions |
| Mocking | NSubstitute or Moq |
| Integration Testing | Testcontainers |

---

## 4.3 Infrastructure

Recommended production stack:

- SQL Server
- Redis
- S3-compatible Object Storage
- CDN
- Firebase Cloud Messaging
- SMS provider
- Payment Gateway
- Docker
- Docker Compose
- Reverse Proxy / Load Balancer
- HTTPS
- CI/CD pipeline
- Centralized structured logs
- Health checks

Kubernetes is **not mandatory initially**.

Introduce Kubernetes only when operational requirements justify it.

---

# 5. Repository Structure

Recommended monorepo:

```text
afraz-studio/
│
├── src/
│   ├── backend/
│   │   ├── Afraz.Api/
│   │   ├── Afraz.Application/
│   │   ├── Afraz.Domain/
│   │   ├── Afraz.Infrastructure/
│   │   └── Afraz.Features/
│   │
│   └── frontend/
│       ├── src/
│       ├── public/
│       └── capacitor/
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
│   ├── afraz-studio-reference.md
│   ├── constitution.md
│   ├── architecture/
│   └── adr/
│
├── docker/
├── docker-compose.yml
├── .editorconfig
├── README.md
└── .gitignore
```

The exact number of .NET projects may be simplified if separation provides no practical value.

**Do not create projects merely to satisfy a diagram.**

---

# 6. Backend Module Boundaries

Initial logical modules:

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

Modules must communicate through explicit application contracts.

Avoid arbitrary cross-module access to another module's persistence internals.

---

# 7. Vertical Slice Constitution

Application behavior SHALL be organized by use case, not by generic technical folders.

Preferred:

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
    ├── GetAvailableSlots/
    ├── CancelBooking/
    └── RescheduleBooking/
```

Avoid:

```text
Controllers/
Services/
Repositories/
Dtos/
Validators/
```

as giant global folders containing unrelated features.

---

# 8. CQRS Constitution

Commands modify state.

Queries read state.

Examples:

```text
CreateBookingCommand
CancelBookingCommand
RescheduleBookingCommand
CreatePrintOrderCommand
CreateAlbumOrderCommand
CreateStoreOrderCommand
ConfirmPaymentCommand
```

Queries:

```text
GetHomeQuery
GetPortfolioQuery
GetAvailableSlotsQuery
GetCustomerBookingsQuery
GetCustomerOrdersQuery
GetOrderGalleryQuery
GetAlbumOptionsQuery
GetStoreProductsQuery
```

CQRS does **not** mean:

- separate databases by default
- event sourcing by default
- Kafka by default
- duplicate read/write infrastructure without reason

Initially, commands and queries may use the same SQL Server database and EF Core DbContext.

---

# 9. Domain Model Rules

Business invariants belong in the domain/application behavior, not in Vue components.

Examples:

- A booking slot cannot be double-booked.
- A customer cannot access another customer's gallery.
- Client-supplied prices are never authoritative.
- A print order can contain only valid customer photos.
- An album can contain only photos the customer is authorized to use.
- Payment callbacks must be idempotent.
- An inactive package cannot be booked.
- An unavailable print size cannot be ordered.

Prefer meaningful domain concepts over anemic CRUD-only models where business rules exist.

Do not force DDD patterns into simple reference/configuration data.

---

# 10. EF Core Constitution

Use:

- EF Core 10
- SQL Server
- Fluent API entity configurations
- Migrations
- Explicit indexes
- Appropriate unique constraints
- Transactions for multi-step critical operations
- Optimistic concurrency where applicable
- `AsNoTracking()` for read-only queries where appropriate
- Projection directly to response models for queries

Do NOT create a generic repository over EF Core by default.

Avoid:

```text
IGenericRepository<T>
GenericRepository<T>
IUnitOfWork
```

unless a concrete requirement proves their value.

`DbContext` already provides repository/unit-of-work behavior.

---

# 11. Database Rules

SQL Server is the system of record for transactional metadata.

Store:

- Customers
- Bookings
- Packages
- Orders
- Gallery metadata
- Print configuration
- Album configuration
- Products
- Payments
- Notifications
- Status histories

Do NOT store original photography binaries in SQL Server.

Required database practices:

- Foreign keys
- Unique constraints
- Proper indexes
- UTC timestamps internally
- Explicit decimal precision for money
- Concurrency protection for booking
- Idempotency constraints for payment processing

Use `decimal`, never floating-point types, for monetary values.

---

# 12. Image & File Architecture

Photography is a core product asset.

Use:

```text
Object Storage
    │
    ├── originals/
    ├── galleries/
    │   ├── thumbnails/
    │   ├── medium/
    │   └── high/
    ├── portfolio/
    ├── products/
    └── album-previews/
```

SQL Server stores metadata and object keys.

Use a CDN for delivery.

The frontend should not automatically download original full-resolution images.

Prefer:

```text
Gallery Grid     → Thumbnail
Photo Viewer     → Medium / High
Explicit Need    → Original / Protected High Resolution
```

Private gallery assets must not become globally public merely because their URL is known.

Use authorization or signed/expiring URLs where appropriate.

---

# 13. Redis Constitution

Redis may be used for:

- Public portfolio cache
- Configuration cache
- Product/category cache
- Album option cache
- Distributed booking locks if required
- Rate limiting
- Short-lived workflow state
- Idempotency assistance

Redis SHALL NOT be the authoritative source for:

- Payments
- Orders
- Bookings
- Customer ownership
- Financial totals

---

# 14. Background Processing

Use Hangfire for durable background work unless Quartz is better suited to a specific scheduling requirement.

Potential jobs:

- Appointment reminders
- SMS notifications
- Push notifications
- Thumbnail generation
- Image optimization
- Expired booking cleanup
- Payment reconciliation
- Album/print status notifications

Long-running work should not block normal HTTP requests.

---

# 15. API Constitution

API style:

- REST
- JSON
- HTTPS
- `/api/...`
- Explicit request/response contracts
- Consistent HTTP status codes
- ProblemDetails errors

Example:

```text
POST /api/auth/login
POST /api/auth/otp/request
POST /api/auth/otp/verify

GET  /api/home

GET  /api/portfolio
GET  /api/portfolio/categories/{id}

GET  /api/photography/services
GET  /api/photography/packages
GET  /api/bookings/availability
POST /api/bookings

GET  /api/bookings/me
GET  /api/bookings/{id}

GET  /api/orders/me
GET  /api/orders/{id}
GET  /api/orders/{id}/gallery

POST /api/print-orders
POST /api/album-orders

GET  /api/store/products
GET  /api/store/products/{id}

POST /api/payments
POST /api/payments/callback
```

Avoid returning EF entities directly from endpoints.

---

# 16. Endpoint Rules

Endpoints/controllers SHALL remain thin.

An endpoint should primarily:

1. Accept HTTP input
2. Map/authenticate context
3. Dispatch command/query
4. Return HTTP response

Do not place significant business logic in controllers/endpoints.

---

# 17. Validation

Use FluentValidation server-side.

Frontend validation improves UX but never replaces backend validation.

Validate:

- Mobile numbers
- Required customer data
- Booking availability
- Package validity
- Date/time
- Photo ownership
- Print configuration
- Album configuration
- Product availability
- Address data
- Payment requests

Prices must always be recalculated/verified by the backend.

---

# 18. Authentication & Authorization

Use:

- JWT access tokens
- Refresh tokens
- Refresh-token rotation
- Revocation support
- Secure token storage strategy appropriate for Capacitor
- Authorization policies where useful

Never authorize a private resource solely because the user knows its ID.

Always verify ownership.

Example:

```text
Customer A
    └── Order 123
          └── Gallery 456
```

Customer B must receive no access to Order 123 or Gallery 456.

---

# 19. WebView / Capacitor Security

The mobile shell must:

- Load only trusted application origins
- Use HTTPS
- Avoid arbitrary navigation to untrusted pages
- Validate deep links
- Protect authentication tokens
- Restrict native bridge capabilities
- Request only necessary device permissions
- Avoid exposing secrets in JavaScript bundles

Native capabilities should be exposed through narrow, explicit interfaces.

---

# 20. Payment Constitution

Payments are business-critical.

All payment logic must be backend-authoritative.

Support:

- Booking deposit
- Full booking payment
- Print payment
- Personalized album payment
- Store checkout payment
- Remaining balance payment

Required guarantees:

- Gateway callback verification
- Idempotent processing
- Duplicate callback safety
- Backend-calculated amount
- Auditability
- Explicit transaction status
- Failure recording
- Reconciliation capability

Never mark an order paid based only on a frontend redirect.

---

# 21. Money Rules

Use a single explicit monetary convention.

The project must document whether persisted values represent:

- Rial
- Toman

Never mix them implicitly.

Recommended:

- Store one canonical unit in backend/database
- Convert only for presentation where required
- Name fields clearly

Example:

```text
Amount
PaidAmount
RemainingAmount
```

with the canonical currency documented globally.

---

# 22. Booking Concurrency

Double booking is prohibited.

The final booking confirmation must be concurrency-safe.

Possible mechanisms:

- SQL unique constraint
- Serializable/appropriate transaction strategy
- Concurrency token
- Short reservation hold
- Redis distributed lock only if actually required

Database constraints should be the final safety boundary where practical.

Do not rely only on a frontend availability check.

---

# 23. Frontend Architecture

Recommended:

```text
src/
├── app/
├── assets/
│   ├── fonts/
│   ├── icons/
│   ├── images/
│   └── patterns/
├── components/
│   ├── ui/
│   └── shared/
├── features/
│   ├── auth/
│   ├── home/
│   ├── booking/
│   ├── orders/
│   ├── gallery/
│   ├── printing/
│   ├── albums/
│   ├── store/
│   ├── payments/
│   └── profile/
├── router/
├── stores/
├── services/
├── composables/
├── styles/
├── types/
└── main.ts
```

Prefer feature-local components and services where they are not shared.

Do not create one enormous global `components/` directory.

---

# 24. Vue Constitution

Use:

- Vue 3
- Composition API
- `<script setup>`
- TypeScript strict mode
- Typed component props/emits
- Composables for reusable behavior
- Pinia only for genuinely shared state

Avoid placing all API data into global stores.

Prefer local state for local UI.

---

# 25. Frontend API Layer

Do not call Axios arbitrarily from every component.

Use feature-level API clients/services.

Example:

```text
features/
└── booking/
    ├── api/
    │   └── booking.api.ts
    ├── components/
    ├── composables/
    ├── pages/
    └── types/
```

Keep server contracts typed.

---

# 26. Frontend Design System

Create reusable tokens.

Example:

```css
:root {
  --brand-primary: #075d69;
  --brand-dark: #03454f;

  --accent-pink: #ff6f91;
  --accent-yellow: #f6bd41;
  --accent-mint: #65d6c3;
  --accent-lilac: #b78ce5;

  --background-primary: #fafaf8;
  --surface-primary: #ffffff;

  --text-primary: #172b2f;
  --text-secondary: #66777a;

  --space-1: 4px;
  --space-2: 8px;
  --space-3: 12px;
  --space-4: 16px;
  --space-6: 24px;
  --space-8: 32px;
}
```

Avoid random hard-coded visual values throughout components.

---

# 27. Typography

The UI must use a Persian sans-serif typeface.

Current design direction:

- Alibaba Regular or approved Persian sans-serif family
- Consistent type scale
- RTL text metrics
- Avoid arbitrary per-component font sizes

Typography tokens should cover:

- Display
- Page title
- Section title
- Body
- Label
- Caption
- Navigation label

---

# 28. Mobile UI Rules

Primary design target:

**iPhone 17 Pro Max**

The implementation must:

- Respect Dynamic Island/top safe area
- Respect bottom home indicator
- Use `env(safe-area-inset-top)`
- Use `env(safe-area-inset-bottom)`
- Support viewport resizing with mobile keyboard
- Prevent bottom navigation from covering content
- Support touch targets of approximately 44pt minimum
- Avoid desktop-first assumptions

---

# 29. RTL Constitution

RTL is mandatory and first-class.

Do not build LTR first and blindly mirror it later.

Review:

- Navigation direction
- Back chevrons
- Horizontal scrolling
- Text alignment
- Calendar direction
- Price layouts
- Form labels
- Icons with directional meaning
- Transitions

Use CSS logical properties where practical:

```css
margin-inline-start
margin-inline-end
padding-inline
inset-inline
border-inline
```

instead of unnecessary `left/right` assumptions.

---

# 30. UI / Visual Direction

The design should be:

- Modern
- Minimal
- Cheerful
- Premium
- Child/newborn photography appropriate
- Photography-first
- Instagram-inspired in scale/hierarchy where useful
- Apple iOS 26-inspired for system/navigation surfaces

Use:

- cheerful but controlled palette
- subtle SVG background graphics
- consistent custom icons
- generous whitespace
- standard section spacing

Do not turn the application into a colorful dashboard.

---

# 31. Liquid Glass

Liquid Glass is primarily a navigation/control treatment.

Use it for:

- Floating bottom navigation
- Selected floating controls
- Sheets/overlays where appropriate

Do not apply glass to every content card.

The bottom navigation should be:

- translucent
- blurred
- floating
- safe-area aware
- legible over varying backgrounds

Example foundation:

```css
.glass-nav {
  background: rgba(255, 255, 255, 0.58);
  backdrop-filter: blur(24px) saturate(180%);
  -webkit-backdrop-filter: blur(24px) saturate(180%);
  border: 1px solid rgba(255, 255, 255, 0.65);
}
```

Actual values should be tuned against real devices.

---

# 32. Accessibility

Required:

- Adequate color contrast
- Keyboard accessibility for web
- Semantic HTML
- Accessible labels
- Screen-reader-friendly controls
- Minimum practical touch target
- Do not communicate state using color alone
- Respect reduced motion when practical
- Meaningful alt text for public content where appropriate

---

# 33. Performance

Photography-heavy pages require deliberate optimization.

Frontend requirements:

- Lazy-load images
- Responsive image sizes
- Thumbnail-first galleries
- Virtualize very large galleries if needed
- Avoid unnecessary global reactive state
- Route-level code splitting
- Cache static/public assets
- Avoid large blocking JS bundles

Backend:

- Project queries to DTOs
- Avoid N+1 queries
- Add indexes based on query patterns
- Cache stable public data
- Paginate large collections
- Do not return entire galleries in one unbounded response

---

# 34. Pagination

Large lists SHALL be paginated or cursor-based where appropriate.

Examples:

- Portfolio
- Customer gallery
- Products
- Orders
- Notifications
- Admin lists

Do not build APIs that return tens of thousands of rows in one request.

---

# 35. Observability

Backend must include:

- Serilog structured logging
- Correlation/Trace ID
- Request logging
- Health checks
- Payment audit logging
- Booking status history
- Order status history

Never log:

- Passwords
- OTP values
- Access tokens
- Refresh tokens
- Sensitive payment credentials

---

# 36. Error Handling

Use centralized exception handling and ProblemDetails.

Do not leak:

- stack traces
- SQL errors
- internal implementation details

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

Frontend should map technical failures to understandable Persian messages.

---

# 37. Configuration & Secrets

Use configuration providers.

Never commit secrets.

Local development:

- User Secrets
- `.env` files excluded from Git where appropriate

Production:

- Environment variables
- Secret manager / platform secrets

Secrets include:

- Database passwords
- JWT signing keys
- SMS credentials
- Payment gateway credentials
- Object storage secrets
- Firebase credentials

---

# 38. Testing Constitution

Business-critical behavior requires automated tests.

## Backend Unit Tests

Focus on:

- Domain calculations
- Pricing
- State transitions
- Validation rules

## Integration Tests

Focus on:

- EF Core mappings
- SQL constraints
- Booking concurrency
- Authentication/authorization
- Payment idempotency
- API contracts

Prefer Testcontainers for realistic infrastructure tests.

## Frontend

Use:

- Vitest
- Vue Test Utils
- Playwright

Critical Playwright flows:

```text
Login
Booking
Reservation payment
Private gallery
Photo selection
Print checkout
Album builder
Store checkout
Remaining balance
```

---

# 39. Architecture Tests

Consider architecture tests to enforce important boundaries.

Examples:

- Domain must not depend on Infrastructure
- Feature boundaries must remain valid
- Application should not reference API presentation concerns

Do not create architecture tests for trivial naming preferences.

---

# 40. Code Quality

Backend:

- Nullable reference types enabled
- Treat important warnings seriously
- Async for I/O
- CancellationToken propagation
- Explicit DTOs/contracts
- Small focused handlers
- No dead code
- No speculative abstractions

Frontend:

- TypeScript strict mode
- ESLint
- Prettier
- No `any` unless explicitly justified
- Reusable design tokens
- Typed API contracts
- Small focused components

---

# 41. Naming

Use English for:

- Source code
- Class names
- Database schema
- API contracts
- Git commits where team convention permits
- Technical documentation

Use Persian for customer-facing UI content.

Examples:

```text
CreateBookingCommand
PhotographyOrder
GalleryPhoto
PrintOrder
AlbumOrder
PaymentTransaction
```

not transliterated Persian class names.

---

# 42. API Versioning

Do not add versioning complexity before it is needed.

When public/mobile compatibility requires independent API evolution, introduce an explicit strategy such as:

```text
/api/v1/...
```

Do not prematurely maintain multiple versions.

---

# 43. Database Migrations

EF Core migrations must:

- Be committed to source control
- Have meaningful names
- Be reviewed before production
- Avoid destructive changes without migration strategy

Never use `EnsureCreated()` for production databases.

---

# 44. Idempotency

Idempotency is mandatory for business operations where duplicate execution can cause financial or order problems.

Especially:

- Payment callbacks
- Payment confirmation
- Order submission
- Potential retry-sensitive external integrations

Use persistent idempotency where correctness requires it.

---

# 45. Status Histories

Important workflows should preserve status history rather than only overwriting the current state.

Recommended:

```text
BookingStatusHistory
PhotographyOrderStatusHistory
PrintOrderStatusHistory
AlbumOrderStatusHistory
StoreOrderStatusHistory
PaymentTransaction
```

This supports:

- audit
- customer support
- debugging
- reporting

---

# 46. Audit Requirements

Audit important administrative actions where appropriate:

- Payment adjustments
- Order status changes
- Gallery publication
- Price changes
- Booking cancellation/rescheduling
- Refunds

Auditability is more important for financial and customer-delivery operations than for ordinary content edits.

---

# 47. CI/CD

Recommended pipeline:

```text
Pull Request
    │
    ├── Restore dependencies
    ├── Build backend
    ├── Backend tests
    ├── Frontend type-check
    ├── Frontend lint
    ├── Frontend tests
    └── Build frontend

Merge
    │
    ├── Build Docker image(s)
    ├── Security/dependency checks
    ├── Deploy
    ├── Run migrations safely
    └── Health verification
```

Production deployments should be repeatable.

---

# 48. Docker

Provide local Docker Compose for infrastructure.

At minimum:

```text
SQL Server
Redis
Object Storage emulator/service if useful
Backend
Frontend (optional for local developer workflow)
```

Do not force developers to containerize the frontend during normal hot-reload development if local Vite is faster.

---

# 49. Environment Separation

Support:

- Development
- Test
- Staging
- Production

Never point development/test applications at production payment or production customer data unintentionally.

Use sandbox payment/SMS integrations where available.

---

# 50. Dependency Policy

Before adding a dependency ask:

1. Does the platform already solve this?
2. Is the library actively maintained?
3. Does it materially simplify the solution?
4. Is its license acceptable?
5. Does it increase bundle/runtime complexity?
6. Is the feature critical enough to own ourselves?

Avoid dependency accumulation.

---

# 51. What We Explicitly Avoid Initially

Unless a concrete requirement emerges, do NOT introduce:

- Microservices
- Kafka
- RabbitMQ
- Event Sourcing
- Separate CQRS databases
- Kubernetes
- Elasticsearch
- GraphQL
- Generic Repository
- Complex service mesh
- Distributed transactions

These can be introduced later based on measurable needs.

---

# 52. Scalability Strategy

Scale the modular monolith vertically/horizontally before decomposing it.

Likely first scaling points:

1. CDN/image delivery
2. Object storage
3. Redis caching
4. SQL query/index optimization
5. Stateless API horizontal scaling
6. Background worker scaling

Only extract services when there is a strong reason such as:

- independent scaling
- operational isolation
- team ownership
- deployment independence
- materially different workload

---

# 53. Security Checklist

Before production:

- [ ] HTTPS enforced
- [ ] Secure headers
- [ ] CORS explicitly configured
- [ ] Rate limiting
- [ ] JWT validation
- [ ] Refresh token rotation
- [ ] Authorization on customer-owned resources
- [ ] Payment callback verification
- [ ] Secrets outside source control
- [ ] Object storage access protected
- [ ] Upload validation
- [ ] File type/size restrictions
- [ ] SQL injection protection through parameterized EF queries
- [ ] Logs scrub sensitive information
- [ ] Admin endpoints protected
- [ ] Dependency vulnerabilities reviewed

---

# 54. Definition of Done — Backend Slice

A backend vertical slice is complete when applicable:

- [ ] Request/command/query exists
- [ ] Handler exists
- [ ] Validation exists
- [ ] Authorization exists
- [ ] Business rules are enforced
- [ ] Persistence is correct
- [ ] Response contract is explicit
- [ ] Errors use ProblemDetails
- [ ] Logging is sufficient
- [ ] Unit/integration tests cover critical behavior
- [ ] OpenAPI contract is correct

---

# 55. Definition of Done — Frontend Feature

A frontend feature is complete when applicable:

- [ ] Matches design system
- [ ] RTL is correct
- [ ] iPhone safe areas are respected
- [ ] Loading state exists
- [ ] Empty state exists
- [ ] Error state exists
- [ ] API errors are understandable
- [ ] Accessibility basics are covered
- [ ] Mobile keyboard behavior is correct
- [ ] Bottom navigation does not cover content
- [ ] Tests cover critical behavior
- [ ] No unnecessary duplicate UI styles

---

# 56. Definition of Done — Business Flow

A flow such as Booking, Printing, Album Builder, or Checkout is not complete merely because the UI exists.

It must cover:

```text
UI
+
Validation
+
API
+
Authorization
+
Persistence
+
Concurrency where relevant
+
Payment where relevant
+
Failure states
+
Retry/idempotency
+
Logging
+
Tests
```

---

# 57. Codex Constitution

When Codex works on this repository, it MUST:

1. Read this constitution before architectural changes.
2. Read `docs/afraz-studio-reference.md` for business requirements.
3. Inspect existing code before creating new patterns.
4. Follow existing conventions unless they conflict with this constitution.
5. Work feature-by-feature / vertical-slice-by-vertical-slice.
6. Avoid broad unrelated refactors.
7. Keep changes reviewable.
8. Add/update tests with business-critical changes.
9. Never remove requirements silently.
10. Never invent new infrastructure without explaining why it is required.
11. Never trust client-calculated prices.
12. Never bypass authorization for private galleries/orders.
13. Preserve RTL and mobile-first requirements.
14. Prefer built-in .NET/Vue capabilities before adding libraries.
15. Update ADRs when making significant architectural decisions.

---

# 58. Codex Planning Rule

For non-trivial work, Codex should first produce a short plan containing:

```text
Goal
Affected modules
Affected files
Database changes
API changes
Frontend changes
Tests
Risks
```

Then implement.

Do not generate hundreds of files before validating the architecture.

---

# 59. Architecture Decision Records

Significant decisions belong in:

```text
docs/adr/
```

Example:

```text
0001-use-modular-monolith.md
0002-use-capacitor.md
0003-use-object-storage-for-photos.md
0004-payment-idempotency-strategy.md
```

ADR format:

```markdown
# Decision

## Context

## Decision

## Consequences

## Alternatives Considered
```

---

# 60. Initial Implementation Milestones

### Milestone 1 — Foundation

- .NET 10 solution
- Vue 3 application
- SQL Server
- EF Core
- Redis
- Docker Compose
- Authentication skeleton
- Error handling
- Logging
- OpenAPI
- RTL design foundation
- Capacitor

### Milestone 2 — Public Experience

- Home
- Portfolio
- Categories
- Studio information

### Milestone 3 — Booking

- Services
- Packages
- Availability
- Reservation
- Payment
- Customer reservation history

### Milestone 4 — Photography Orders

- Orders
- Galleries
- Secure photo access
- Favorites

### Milestone 5 — Printing

- Photo selection
- Sizes
- Pricing
- Print orders
- Payment

### Milestone 6 — Album Builder

- Configuration
- Photo selection
- Pricing
- Preview
- Order/payment

### Milestone 7 — Store

- Catalog
- Product details
- Cart
- Checkout
- Delivery
- Payment

### Milestone 8 — Operations

- Notifications
- Support
- Administration
- Reporting/audit improvements

---

# 61. Canonical Stack Summary

```text
MOBILE SHELL
Capacitor
iOS / Android WebView

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
Custom SVG Icons
Vitest
Vue Test Utils
Playwright

BACKEND
.NET 10
ASP.NET Core Web API
Clean Architecture
Modular Monolith
Vertical Slice Architecture
CQRS
MediatR
FluentValidation
EF Core 10
SQL Server
JWT + Refresh Token
ASP.NET Core Identity
ProblemDetails
OpenAPI
Serilog
xUnit
FluentAssertions
Testcontainers

INFRASTRUCTURE
Redis
S3-compatible Object Storage
CDN
Hangfire
Firebase Cloud Messaging
SMS Provider
Payment Gateway
Docker
Docker Compose
CI/CD
HTTPS
Health Checks
Structured Logging
```

---

# 62. Final Rule

The system should begin as a **well-structured modular monolith**, not a distributed system.

Every technical decision should answer:

> Does this make the Afraz Studio business safer, simpler, easier to maintain, or measurably faster?

If the answer is no, do not add the complexity.

**Business requirements are the source of features.  
This constitution is the source of engineering rules.**
