# AGENTS.md — Afraz Studio

## Purpose

This file defines the operating instructions for AI coding agents working in the **Afraz Studio** repository.

Before making non-trivial changes, read:

1. `docs/reference.md` — business/product requirements
2. `docs/constitution.md` and `docs/architecture.md` — engineering rules and architecture
4. Relevant ADRs under `docs/adr/`
5. UI/UX Designs in `docs\design`
6. Existing code in the affected feature/module
7. Backend code base in `src\backend`
8. Frontend code base in `src\frontend`

If these documents conflict:

1. Explicit user instruction wins.
2. Accepted ADRs override older architectural guidance.
3. The engineering constitution governs technical decisions.
4. The product reference governs business requirements.
5. Existing implementation conventions should be preserved when they do not conflict with the above.

Do not silently remove, weaken, or reinterpret business requirements.

---

# 1. Project Summary

Afraz Studio is a Persian RTL application for a baby/child photography studio.

Primary capabilities include:

- Authentication
- Customer profiles
- Public photography portfolio
- Photography booking and scheduling
- Reservation/deposit payments
- Photography orders
- Private customer galleries
- Photo selection for printing
- Print size/quantity configuration
- Print payments
- Personalized physical album creation
- Album payment
- Online album/product store
- Cart and checkout
- Remaining balance payments
- Notifications
- Studio/contact/legal information
- Support
- Administration

The customer frontend is delivered primarily as a Vue application inside a Capacitor/WebView mobile shell.

Primary design target: **iPhone 17 Pro Max**.

All customer-facing UI is Persian and RTL unless explicitly stated otherwise.

---

# 2. Canonical Technology Stack

## Frontend

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
- Lucide / Material Symbols where useful
- Vitest
- Vue Test Utils
- Playwright

## Backend

- .NET 10
- ASP.NET Core Web API
- Clean Architecture
- Modular Monolith
- Vertical Slice Architecture
- CQRS
- MediatR
- FluentValidation
- EF Core 10
- SQL Server
- JWT + Refresh Token
- ASP.NET Core Identity where appropriate
- ProblemDetails
- OpenAPI
- Serilog
- xUnit
- FluentAssertions
- Testcontainers

## Infrastructure

- Redis
- S3-compatible Object Storage
- CDN
- Hangfire
- Firebase Cloud Messaging
- SMS provider
- Payment gateway
- Docker
- Docker Compose
- CI/CD
- HTTPS
- Health Checks

Do not introduce replacement frameworks or infrastructure without a concrete reason.

---

# 3. Architecture Rules

Start and remain a **Modular Monolith** unless an explicit architectural decision changes this.

Use:

```text
Clean Architecture
        +
Modular Monolith
        +
Vertical Slice Architecture
        +
CQRS
```

Do NOT introduce by default:

- Microservices
- Kafka
- RabbitMQ
- Event Sourcing
- Separate CQRS databases
- Kubernetes
- Elasticsearch
- GraphQL
- Generic Repository
- Distributed transactions
- Service mesh

Additional infrastructure requires a demonstrated business, scale, operational, or reliability need.

---

# 4. Backend Organization

Prefer feature/use-case organization.

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
    ├── GetAvailableSlots/
    ├── CancelBooking/
    └── RescheduleBooking/
```

Do not build giant global folders such as:

```text
Controllers/
Services/
Repositories/
Dtos/
Validators/
```

containing unrelated business features.

Keep each vertical slice cohesive.

---

# 5. CQRS Rules

Commands mutate state.

Queries read state.

Examples:

```text
CreateBookingCommand
CancelBookingCommand
CreatePrintOrderCommand
CreateAlbumOrderCommand
ConfirmPaymentCommand
```

```text
GetHomeQuery
GetAvailableSlotsQuery
GetCustomerOrdersQuery
GetOrderGalleryQuery
GetStoreProductsQuery
```

CQRS does not imply separate persistence systems.

Initially, commands and queries may use the same SQL Server database and EF Core DbContext.

---

# 6. EF Core Rules

Use EF Core directly unless a specific abstraction provides demonstrated value.

Do NOT create:

```text
IGenericRepository<T>
GenericRepository<T>
```

by default.

Use:

- Fluent entity configuration
- Migrations
- Explicit indexes
- Foreign keys
- Unique constraints
- Transactions where necessary
- Optimistic concurrency where useful
- `AsNoTracking()` for appropriate read paths
- Projection to response DTOs
- Async database APIs
- CancellationToken propagation

Never use `EnsureCreated()` for production databases.

Never store photography binaries in SQL Server.

---

# 7. Business Correctness Rules

These are critical invariants.

## Booking

- Never double-book a time slot.
- Frontend availability is informational; backend/database validation is authoritative.
- Booking price is calculated by the backend.
- Inactive services/packages cannot be booked.
- Payment-dependent bookings are not confirmed solely from frontend redirects.

## Galleries

- Customers may access only their own private galleries.
- Knowing an order/gallery/photo ID does not grant access.
- Every private asset request must enforce ownership/authorization.

## Printing

- Selected photos must belong to the authenticated customer.
- Print sizes must be active and valid.
- Quantity must be valid.
- Prices are calculated by the backend.

## Albums

- Album configuration must be valid.
- Selected photos must be authorized for the customer.
- Album price is calculated by the backend.

## Store

- Product price is backend-authoritative.
- Product/variant availability must be checked during checkout.
- Inventory must be validated before order confirmation if inventory tracking is enabled.

## Payments

- Payment verification is backend-only.
- Callbacks must be verified.
- Processing must be idempotent.
- Duplicate callbacks must never duplicate business effects.
- Never trust payment amount supplied by the client.

---

# 8. Money Rules

Use `decimal` in .NET and appropriate SQL decimal precision.

Never use `float` or `double` for monetary calculations.

The repository must use one documented canonical monetary unit.

Never implicitly mix Rial and Toman.

Frontend displays values; backend owns calculations.

---

# 9. API Rules

Use REST/JSON over HTTPS.

Endpoints should be thin.

An endpoint should primarily:

1. Parse HTTP input
2. Resolve authentication/context
3. Dispatch command/query
4. Return HTTP result

Business logic belongs in the appropriate slice/domain behavior.

Never return EF entities directly.

Use explicit request/response contracts.

Use ProblemDetails for API failures.

Do not expose stack traces, SQL messages, secrets, or internal implementation details.

---

# 10. Validation Rules

Use FluentValidation on backend request/use-case boundaries.

Frontend validation is for UX only and does not replace backend validation.

Validate at minimum where relevant:

- Mobile number
- Required customer information
- Booking date/time
- Slot availability
- Service/package validity
- Photo ownership
- Print options
- Album configuration
- Product/variant availability
- Address
- Payment request

---

# 11. Authentication & Security

Use:

- JWT access tokens
- Refresh tokens
- Refresh-token rotation
- Token revocation strategy
- HTTPS
- Authorization policies where appropriate
- Rate limiting
- Secure secret management

Never log:

- Passwords
- OTP codes
- JWTs
- Refresh tokens
- Payment secrets

Never commit secrets.

Use environment configuration, User Secrets, or production secret management.

---

# 12. Photography Storage

Use object storage for images/files.

Conceptual structure:

```text
Object Storage
├── originals/
├── galleries/
│   ├── thumbnails/
│   ├── medium/
│   └── high/
├── portfolio/
├── products/
└── album-previews/
```

SQL Server stores metadata/object keys.

Use optimized variants for normal app display.

Do not load originals into gallery grids.

Use CDN delivery where appropriate.

Private assets should use protected or signed/expiring access when required.

---

# 13. Redis Rules

Redis is optional infrastructure for appropriate workloads, including:

- Cache
- Rate limiting
- Short-lived workflow state
- Booking coordination/locks if needed
- Idempotency assistance

Redis is not the source of truth for:

- Orders
- Bookings
- Payments
- Financial values
- Ownership

---

# 14. Background Jobs

Use Hangfire for durable asynchronous/background tasks unless another documented decision exists.

Examples:

- Appointment reminders
- Push notifications
- SMS
- Image processing
- Thumbnail generation
- Payment reconciliation
- Expired booking cleanup

Do not block HTTP requests with long-running processing.

---

# 15. Frontend Architecture

Prefer feature-oriented organization.

Example:

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

Keep feature-specific code inside the feature when it is not genuinely shared.

Do not create oversized global component/service directories.

---

# 16. Vue Rules

For new code:

- Use Vue 3 Composition API.
- Use `<script setup lang="ts">`.
- Enable strict TypeScript.
- Type props and emits.
- Prefer composables for reusable behavior.
- Use Pinia only for shared application state.
- Keep local UI state local.
- Avoid `any` unless justified.
- Do not call Axios directly from arbitrary presentation components.

Use feature-level API clients.

Example:

```text
features/booking/
├── api/
│   └── booking.api.ts
├── components/
├── composables/
├── pages/
└── types/
```

---

# 17. UI & Design System Rules

Customer UI must be:

- Persian
- RTL
- Mobile-first
- iPhone-first
- Modern
- Minimal
- Cheerful
- Photography-focused
- Suitable for newborn/child photography

Primary target: **iPhone 17 Pro Max**.

Respect:

- Dynamic Island
- Top safe area
- Bottom home indicator
- WebView safe areas
- Mobile keyboard
- Touch targets

Use:

```css
env(safe-area-inset-top)
env(safe-area-inset-bottom)
```

where appropriate.

---

# 18. RTL Rules

RTL is first-class.

Do not implement LTR and blindly mirror afterward.

Check:

- Back navigation
- Chevrons
- Horizontal lists
- Gallery direction
- Calendar
- Forms
- Price layouts
- Animations/transitions
- Bottom navigation
- Directional icons

Prefer CSS logical properties:

```text
margin-inline-start
margin-inline-end
padding-inline
inset-inline
```

---

# 19. Typography & Icons

Use the approved Persian sans-serif family.

Keep typography scale centralized in the design system.

Do not hard-code random font sizes across screens.

Icons should:

- Use a consistent visual weight
- Match the established design system
- Be custom SVG where business identity benefits
- Use Lucide/Material Symbols only as a consistent base
- Maintain appropriate mobile touch targets

---

# 20. Liquid Glass

Liquid Glass is primarily for functional/navigation surfaces.

The floating bottom navigation should use a translucent, blurred glass treatment while remaining readable.

Do not apply glass to every content card.

Content and photography should remain visually clear.

---

# 21. Frontend Performance

This is an image-heavy application.

Agents must consider:

- Lazy loading
- Responsive images
- Thumbnail-first galleries
- Route-level code splitting
- Pagination
- Virtualization for very large galleries where justified
- Bundle size
- Avoiding unnecessary global reactivity

Never return or render an unbounded private gallery.

---

# 22. Error / Loading / Empty States

A feature is not complete with only the happy path.

Where applicable implement:

- Loading
- Skeleton
- Empty
- Error
- Offline
- Retry
- Unauthorized/expired session
- Payment processing
- Payment failure
- Success

Customer-facing messages must be understandable Persian.

---

# 23. Accessibility

Use:

- Semantic HTML
- Accessible labels
- Adequate contrast
- Keyboard accessibility where relevant
- Practical minimum touch targets around 44pt
- Reduced-motion consideration
- State indicators beyond color alone

---

# 24. Logging & Observability

Use Serilog structured logging.

Include correlation/trace IDs.

Log important workflow transitions.

Maintain status history/audit data for business-critical operations where required.

Never log sensitive credentials or tokens.

---

# 25. Testing Rules

Do not chase meaningless coverage percentages.

Test business risk.

Backend critical tests include:

- Double-book prevention
- Booking pricing
- Authorization
- Private gallery ownership
- Print pricing
- Album pricing
- Payment idempotency
- Payment verification
- Order state transitions

Use:

- xUnit
- FluentAssertions
- Testcontainers for integration tests

Frontend:

- Vitest
- Vue Test Utils
- Playwright

Critical E2E flows include:

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

# 26. Code Quality Rules

## Backend

- Nullable reference types enabled
- Async I/O
- CancellationToken propagated
- Explicit contracts
- Small handlers
- No speculative abstraction
- No dead code
- Avoid unnecessary reflection/magic

## Frontend

- TypeScript strict mode
- ESLint
- Prettier
- Small focused components
- Typed API contracts
- Centralized design tokens
- No duplicated visual systems

---

# 27. Naming Rules

Technical code and identifiers should be English.

Customer-facing UI is Persian.

Good:

```text
PhotographyOrder
CreateBookingCommand
GalleryPhoto
PrintOrder
AlbumOrder
PaymentTransaction
```

Avoid transliterated Persian identifiers.

---

# 28. Database Changes

For schema changes:

1. Update entity/model.
2. Update EF configuration.
3. Add/review migration.
4. Add/update indexes and constraints.
5. Consider backward compatibility/data migration.
6. Add tests where business-critical.

Never casually delete production data.

---

# 29. Dependency Rules

Before adding a package:

1. Check whether the existing stack/platform already solves it.
2. Verify maintenance status.
3. Verify license.
4. Evaluate bundle/runtime cost.
5. Explain why the dependency is needed.

Do not accumulate libraries for trivial utilities.

---

# 30. Scope Discipline

When given a task:

- Change only what is necessary.
- Do not perform broad unrelated refactors.
- Do not rename unrelated files.
- Do not replace architecture while implementing a small feature.
- Preserve public contracts unless the task requires a breaking change.
- Keep diffs reviewable.

If existing code has unrelated problems, mention them separately rather than silently expanding scope.

---

# 31. Working Procedure for Agents

For every non-trivial task:

### Step 1 — Understand

Read:

- User request
- Product reference
- Constitution
- Relevant ADRs
- Relevant existing code/tests

### Step 2 — Inspect

Identify:

- Feature/module
- Existing conventions
- Data model
- API contracts
- Tests
- Dependencies

### Step 3 — Plan

Provide/maintain a concise plan:

```text
Goal
Affected modules/files
Database changes
API changes
Frontend changes
Tests
Risks
```

### Step 4 — Implement

Work one coherent slice at a time.

### Step 5 — Validate

Run relevant:

- Build
- Tests
- Type checking
- Lint
- Formatting

### Step 6 — Review

Before completion check:

- Business correctness
- Authorization
- Validation
- Error states
- RTL
- Tests
- No unrelated changes

### Step 7 — Report

Summarize:

- What changed
- Important decisions
- Tests executed
- Known limitations/follow-ups

---

# 32. Commands

Agents should inspect repository scripts before assuming commands.

Typical backend commands may include:

```bash
dotnet restore
dotnet build
dotnet test
dotnet ef database update
```

Typical frontend commands may include:

```bash
npm install
npm run dev
npm run build
npm run type-check
npm run lint
npm run test
```

Use the package manager already selected by the repository.

Do not switch package managers without explicit reason.

---

# 33. Definition of Done

A change is not done merely because it compiles.

Where applicable it must include:

```text
Business behavior
+ Validation
+ Authorization
+ Persistence
+ API contract
+ UI states
+ Error handling
+ Concurrency/idempotency
+ Logging
+ Tests
```

For frontend changes also verify:

```text
RTL
Mobile layout
Safe areas
Loading
Empty
Error
Accessibility
```

---

# 34. Architectural Decisions

For significant decisions create/update an ADR under:

```text
docs/adr/
```

Use:

```markdown
# Decision

## Context

## Decision

## Consequences

## Alternatives Considered
```

Examples requiring an ADR:

- Moving from modular monolith to services
- Changing payment architecture
- Replacing SQL Server
- Introducing message broker
- Changing authentication model
- Changing object storage strategy
- Major frontend framework change

---

# 35. Git / Change Hygiene

Agents should:

- Keep commits/diffs focused.
- Avoid generated noise.
- Never commit secrets.
- Never commit local environment credentials.
- Do not modify lockfiles unless dependencies changed.
- Do not rewrite migration history casually.
- Do not force-format unrelated files.

When asked to commit, use a concise descriptive commit message.

---

# 36. Prohibited Shortcuts

Never:

- Trust client prices
- Skip ownership checks
- Mark payment successful from frontend state
- Expose private gallery assets publicly without protection
- Store original photography binaries in SQL Server
- Ignore double-booking concurrency
- Hard-code production secrets
- Swallow exceptions silently
- Return stack traces to customers
- Disable validation to make a flow pass
- Remove failing tests instead of fixing behavior
- use `any` broadly to silence TypeScript
- create generic repositories merely for architectural ceremony

---

# 37. Initial Development Order

Unless the user requests another order:

```text
1. Foundation
2. Authentication
3. Public Home & Portfolio
4. Booking
5. Payments
6. Photography Orders
7. Private Galleries
8. Printing
9. Album Builder
10. Store & Checkout
11. Notifications
12. Support
13. Administration
```

Implement vertically.

Do not scaffold every future feature with empty placeholder classes before it is needed.

---

# 38. Agent Final Checklist

Before finishing a task ask:

- [ ] Did I follow the business reference?
- [ ] Did I follow the constitution?
- [ ] Did I inspect existing code first?
- [ ] Is the solution simpler than unnecessary alternatives?
- [ ] Are business invariants enforced server-side?
- [ ] Is authorization correct?
- [ ] Are prices backend-controlled?
- [ ] Are payments idempotent where relevant?
- [ ] Is booking concurrency safe where relevant?
- [ ] Is private gallery ownership enforced?
- [ ] Are API errors consistent?
- [ ] Are RTL/mobile requirements preserved?
- [ ] Did I add/update meaningful tests?
- [ ] Did I run relevant validation commands?
- [ ] Did I avoid unrelated changes?
- [ ] Does documentation/ADR need updating?

---

# 39. Final Instruction

Treat:

```text
docs/afraz-studio-reference.md
```

as the source of truth for **what the product must do**.

Treat:

```text
docs/afraz-studio-constitution.md
```

as the source of truth for **how the system must be engineered**.

Treat this:

```text
AGENTS.md
```

as the source of truth for **how coding agents must work inside the repository**.

When uncertain, prefer the smallest solution that:

- preserves business correctness,
- protects customer data,
- follows the established architecture,
- remains easy to understand,
- and can be extended without premature distributed-system complexity.
