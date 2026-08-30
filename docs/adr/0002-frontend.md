# 0002-use-vue-tailwind-component-driven-design-system.md

## Status

Accepted

## Context

Afraz Studio uses a Vue-based frontend delivered as a mobile-first WebView application through Capacitor, while remaining compatible with normal web hosting.

The frontend is expected to implement a highly customized Persian RTL design system with:

- Vue 3
- TypeScript
- Vite
- Tailwind CSS
- Pinia
- Vue Router
- Axios
- VeeValidate
- Zod
- Motion Vue
- Capacitor
- Custom SVG icons
- iPhone-safe-area support
- Persian RTL typography
- A reusable component library
- A consistent design-token system

The application must not evolve into a collection of page-specific styles and duplicated components.

This document defines the frontend engineering standard that all contributors and coding agents must follow.

---

# 1. Decision

The frontend SHALL use a **component-driven, token-based, feature-oriented architecture**.

The implementation principles are:

```text
Vue 3
+
TypeScript
+
Composition API
+
Tailwind CSS
+
Design Tokens
+
Reusable UI Components
+
Feature Modules
+
Strict RTL/Mobile-First Rules
```

The design system is a first-class part of the codebase.

Pages must be composed from reusable design-system primitives and feature components rather than being implemented as isolated one-off layouts.

---

# 2. Technology Standard

## Core

- Vue 3
- TypeScript
- Vite
- Composition API
- `<script setup lang="ts">`

## Styling

- Tailwind CSS
- CSS Variables for design tokens
- Minimal scoped CSS only when Tailwind is not sufficient
- No inline style attributes except for truly dynamic values

## State

- Pinia for shared application state
- Local component/composable state for local UI state

## Routing

- Vue Router

## HTTP

- Axios through a centralized client and feature-specific API modules

## Forms

- VeeValidate
- Zod

## Animation

- Motion Vue
- Native CSS transitions where simpler

## Mobile

- Capacitor
- Safe-area support
- WebView-compatible navigation and lifecycle behavior

---

# 3. Frontend Architecture

Use a feature-oriented structure.

Recommended:

```text
src/
├── app/
│   ├── AppShell.vue
│   ├── providers/
│   └── app.config.ts
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
│   ├── checkout/
│   ├── payments/
│   ├── notifications/
│   └── profile/
├── router/
├── stores/
├── services/
├── composables/
├── styles/
├── types/
├── App.vue
└── main.ts
```

Do not create large technical buckets containing unrelated features.

---

# 4. Feature Structure

Example:

```text
features/
└── booking/
    ├── api/
    │   └── booking.api.ts
    ├── components/
    │   ├── BookingPackageCard.vue
    │   ├── BookingCalendar.vue
    │   └── BookingTimeSlot.vue
    ├── composables/
    │   └── useBooking.ts
    ├── pages/
    │   ├── BookingServicePage.vue
    │   ├── BookingPackagePage.vue
    │   ├── BookingDatePage.vue
    │   └── BookingReviewPage.vue
    ├── schemas/
    │   └── booking.schema.ts
    └── types/
        └── booking.types.ts
```

Feature-specific code stays with the feature unless it is genuinely reusable.

---

# 5. Component Layers

Use three conceptual component levels.

## 5.1 UI Primitives

Low-level reusable components with no business knowledge.

Examples:

```text
AppButton
AppIconButton
AppInput
AppTextarea
AppSelect
AppCheckbox
AppRadio
AppSwitch
AppBadge
AppAvatar
AppDivider
AppSkeleton
AppModal
AppBottomSheet
AppSheet
AppToast
AppTabs
AppChip
AppCard
AppImage
AppEmptyState
AppErrorState
```

These belong in:

```text
components/ui/
```

## 5.2 Shared Composite Components

Examples:

```text
SectionHeader
GlassBottomNav
AppHeader
SearchField
PriceDisplay
StatusBadge
PhotoThumbnail
ProductPrice
LoadingSection
```

These belong in:

```text
components/shared/
```

## 5.3 Feature Components

Examples:

```text
BookingPackageCard
OrderSummaryCard
GalleryPhotoSelector
AlbumOptionSelector
StoreProductCard
PaymentSummary
```

These belong inside the corresponding feature.

---

# 6. Component Design Rules

Every reusable component should:

- Have a clear single responsibility
- Use typed props
- Use typed emits
- Support required states
- Avoid hidden business side effects
- Avoid direct API calls unless it is explicitly a smart feature container
- Use design tokens
- Support RTL
- Support mobile touch sizes

Prefer strongly typed component APIs.

---

# 7. Smart vs Presentational Components

Prefer presentational UI components.

Presentational components:

```text
receive data
render state
emit user actions
avoid fetching data directly
```

Feature/page containers may:

```text
call API
manage feature state
coordinate navigation
compose components
```

Do not make every small component fetch its own data.

---

# 8. Design Tokens

All visual fundamentals must be centralized.

Recommended:

```text
styles/
├── tokens.css
├── typography.css
├── globals.css
├── safe-area.css
└── utilities.css
```

Example:

```css
:root {
  --color-brand-primary: #075d69;
  --color-brand-dark: #03454f;
  --color-accent-pink: #ff6b8a;
  --color-accent-yellow: #ffc857;
  --color-accent-mint: #7dd3c7;
  --color-accent-blue: #8fbafb;
  --color-background: #f8fafb;
  --color-surface: #ffffff;
  --color-text-primary: #172b2f;
  --color-text-secondary: #66777a;
  --color-border-subtle: #e7e9e7;
  --space-1: 4px;
  --space-2: 8px;
  --space-3: 12px;
  --space-4: 16px;
  --space-6: 24px;
  --space-8: 32px;
  --space-10: 40px;
  --space-12: 48px;
  --radius-sm: 8px;
  --radius-control: 12px;
  --radius-card: 16px;
  --radius-lg: 20px;
  --radius-xl: 24px;
}
```

Tailwind should reference these tokens where practical.

Do not scatter raw hex values throughout templates.

---

# 9. Tailwind Rules

Tailwind is the primary styling mechanism.

Preferred:

```vue
<div class="flex items-center gap-4 px-4 py-3">
```

Avoid excessive custom CSS for common layout and spacing.

Use custom CSS when:

- Tailwind cannot express the behavior cleanly
- Implementing Liquid Glass
- Handling safe-area utilities
- Implementing custom animation/effects

Avoid arbitrary-value abuse such as frequent `mt-[13px]`, `text-[17.3px]`, or `z-[99999]`.

Prefer tokenized values.

---

# 10. Typography

Use the approved Persian sans-serif typeface.

Typography must be tokenized.

Recommended semantic scale:

```text
Display
PageTitle
SectionTitle
CardTitle
Body
Label
Caption
NavigationLabel
```

Do not choose font sizes independently on each page.

The scale should remain consistent across:

- Bottom navigation
- Services
- Categories
- Cards
- Forms
- Headers
- Modals

---

# 11. RTL Standard

RTL is mandatory.

Set:

```html
<html lang="fa" dir="rtl">
```

Prefer logical CSS properties and Tailwind start/end utilities.

Directional icons and transitions must be reviewed in RTL.

---

# 12. Mobile-First Standard

Primary target:

**iPhone 17 Pro Max**

Design mobile first.

Every page, routed view, feature screen, and shared layout **SHALL be implemented responsively and mobile first**. This is a mandatory acceptance criterion, not an optional enhancement.

Implementations must:

- Start with the smallest supported mobile viewport as the default layout
- Progressively enhance the layout for larger mobile, tablet, and desktop viewports
- Avoid fixed-width layouts that cause clipping or horizontal overflow
- Keep content, forms, navigation, dialogs, images, and touch targets usable at every supported viewport size
- Verify responsive behavior at representative mobile, tablet, and desktop breakpoints before completion

The iPhone 17 Pro Max is the primary design target, but pages must not be hard-coded exclusively for that viewport.

Respect:

```css
env(safe-area-inset-top)
env(safe-area-inset-bottom)
```

Do not start from desktop and shrink later.


### Mobile-First & Responsive Design:

The application MUST be implemented using a strict **mobile-first approach**.

The mobile design is the primary design and the main source of truth.
Desktop and tablet layouts are secondary adaptations of the mobile experience.

Requirements:

- Start all layouts and components from the mobile viewport.
- The provided mobile reference image is the primary visual source of truth.
- Implement the base CSS/Tailwind classes for mobile first.
- Use responsive breakpoints only to progressively enhance the layout for larger screens.
- Do NOT design desktop first and then shrink it for mobile.
- Do NOT treat the mobile version as a simplified version of desktop.
- Preserve the full functionality and visual quality on mobile.
- All components must be responsive.
- Avoid fixed widths that cause horizontal overflow.
- Use fluid sizing, responsive grids, `max-width`, `min()`, `clamp()`, and responsive Tailwind utilities where appropriate.
- Images must resize/crop responsively without breaking their intended composition.
- Typography and spacing may scale progressively on larger screens.
- Touch targets on mobile must be at least 44×44px.
- Respect iOS/Android safe areas.
- The bottom navigation is primarily designed for mobile.
- On tablet/desktop, navigation and layout may adapt when appropriate, while preserving the same design system.
- Desktop layouts should take advantage of additional space rather than simply stretching mobile components.


---

# 13. Dynamic Island & Safe Area

The application shell must never overlap the Dynamic Island or the bottom home indicator.

Use safe-area-aware spacing rather than hard-coded system-bar assumptions.

---

# 14. Bottom Navigation

The bottom navigation is a shared design-system component.

It should:

- Be floating
- Use Liquid Glass treatment
- Respect the bottom safe area
- Use outline icons when inactive
- Use filled icons when selected
- Use consistent icon sizing and label typography
- Remain legible over changing page backgrounds

Do not reimplement navigation per page.

---

# 15. Icon System

Icons must have a consistent visual language.

Rules:

- Inactive = outline
- Selected = filled
- Consistent stroke width
- Consistent bounding box
- Consistent optical size
- Custom SVGs where the business identity benefits
- No arbitrary mixing of unrelated icon styles

Prefer a shared `AppIcon` abstraction where useful.

---

# 16. Images

Photography is a primary visual asset.

Use shared image behavior:

- Lazy loading
- Correct `object-fit`
- Placeholder/skeleton
- Error fallback
- Responsive image size
- Optimized image variants

Do not load original high-resolution files in normal lists/grids.

---

# 17. Buttons

Create button variants centrally.

Variants:

```text
Primary
Secondary
Ghost
Danger
Glass
```

States:

```text
Default
Pressed
Disabled
Loading
```

Sizes:

```text
Small
Medium
Large
```

Do not create one-off button components per feature.

---

# 18. Form Controls

All forms must use shared primitives.

Required primitives:

```text
AppInput
AppTextarea
AppSelect
AppCheckbox
AppRadio
AppSwitch
```

Each should support:

- Label
- Hint
- Error
- Disabled
- Required
- RTL
- Accessible labeling

---

# 19. Forms & Validation

Use:

- VeeValidate
- Zod

Schemas belong to features.

Example:

```text
features/auth/schemas/login.schema.ts
features/booking/schemas/booking.schema.ts
```

Frontend validation improves UX but is never authoritative for business rules.

---

# 20. API Client Standard

Create one centralized Axios client:

```text
services/http/api-client.ts
```

Responsibilities:

- Base URL
- Standard headers
- Auth token
- Refresh strategy
- Error normalization
- Correlation headers where required

Feature-specific API calls belong under feature folders.

Do not call raw Axios directly from presentational components.

---

# 21. API Contract Typing

Every API request/response should be typed.

Avoid `any`.

Use explicit contracts and shared types when genuinely shared.

---

# 22. Pinia Standard

Pinia should contain only genuinely shared state.

Good examples:

```text
authStore
cartStore
notificationStore
appStore
```

Local UI state remains local.

---

# 23. Composables

Use composables for reusable behavior.

Examples:

```text
useAuth
useSafeArea
useDebounce
usePagination
useImageViewer
usePaymentReturn
```

Do not turn composables into hidden global service locators.

---

# 24. Routing

Use route-level lazy loading.

Do not eagerly load all feature pages into the initial bundle.

---

# 25. Loading / Empty / Error States

Every async feature must define appropriate states.

Use shared primitives for:

- Skeleton loading
- Empty state
- Error state
- Retry
- Offline/session-expired states where relevant

Do not leave blank screens while data loads.

---

# 26. Modals & Bottom Sheets

Prefer shared modal/sheet components.

All overlays should:

- Respect safe areas
- Be accessible
- Support close/back behavior
- Use consistent animation
- Avoid page-specific implementations

---

# 27. Motion Standard

Motion should improve comprehension, not delay interaction.

Use Motion Vue selectively for:

- Bottom sheets
- Selected states
- Gallery transitions
- Navigation states
- Small enter/leave transitions

Respect reduced-motion preferences where practical.

---

# 28. Liquid Glass Standard

Liquid Glass is primarily a navigation/control surface.

Use for:

- Floating bottom navigation
- Floating controls
- Approved overlays

Do not make every card glass.

Example foundation:

```css
.liquid-glass {
  background: rgba(255, 255, 255, 0.58);
  backdrop-filter: blur(24px) saturate(180%);
  -webkit-backdrop-filter: blur(24px) saturate(180%);
  border: 1px solid rgba(255, 255, 255, 0.65);
}
```

Tune values on real devices.

---

# 29. SVG Background Pattern System

Decorative SVG patterns belong in:

```text
assets/patterns/
```

They should be reusable, lightweight, non-interactive, and must not reduce text readability.

Do not duplicate large SVG markup across pages.

---

# 30. Accessibility Standard

Interactive components require:

- Semantic elements where possible
- Accessible names
- Keyboard support where relevant
- Visible focus state
- Adequate contrast
- Practical mobile touch target size
- Selected state not communicated by color alone

---

# 31. Naming

Use PascalCase for Vue components.

Good:

```text
AppButton.vue
BookingPackageCard.vue
GalleryPhotoItem.vue
```

Avoid ambiguous names such as `Box.vue`, `Item.vue`, `Thing.vue`, `Common.vue`.

Recommended related filenames:

```text
booking.api.ts
booking.types.ts
booking.schema.ts
useBooking.ts
BookingPage.vue
BookingPackageCard.vue
```

---

# 32. Props & Events

Avoid broad props like:

```text
config: any
data: object
options: any
```

Prefer explicit typed contracts.

Avoid components with dozens of unrelated booleans; split responsibilities instead.

Use clear event names such as:

```text
submit
select
close
confirm
remove
change
```

---

# 33. CSS Scope

Tailwind should solve most presentation concerns.

Use scoped CSS only for component-specific behavior that is awkward in Tailwind.

Global CSS belongs under `styles/`.

Do not add unrelated global selectors from feature components.

---

# 34. Z-Index

Use a centralized z-index scale.

Example semantic layers:

```text
content
sticky
header
nav
dropdown
overlay
modal
toast
```

Do not use arbitrary extreme z-index values.

---

# 35. Spacing Standard

Use a shared spacing scale.

Recommended:

```text
4
8
12
16
24
32
40
48
```

Page section spacing should be consistent across the app.

---

# 36. Performance Standard

Use:

- Lazy routes
- Lazy images
- Optimized image variants
- Paginated APIs
- Virtualized lists when justified
- Minimal global reactive state

Avoid:

- Huge initial bundles
- Unbounded galleries
- Unnecessary original image downloads
- Heavy animation libraries for trivial effects

---

# 37. Testing Standard

Use:

- Vitest
- Vue Test Utils
- Playwright

Test meaningful behavior:

- Component states
- Validation
- Critical composables
- Navigation behavior
- Selection state
- Design-system states

Critical business flows should have Playwright coverage.

---

# 38. No Business Authority in the Frontend

The frontend may guide the user, but the backend remains authoritative for:

- Prices
- Ownership
- Payments
- Booking availability
- Valid album configurations
- Product availability

Do not encode authoritative business decisions only in Vue components.

---

# 39. Reuse Rule

Before creating a new component:

1. Search the design system.
2. Check existing shared components.
3. Check feature-local components.
4. Reuse or extend if conceptually identical.
5. Create a new abstraction only when truly distinct.

Do not duplicate components because they appear on different pages.

---

# 40. Avoid Premature Generalization

Do not create a generic component after only one use case.

Preferred progression:

```text
implement clear component
→ observe real reuse
→ extract shared abstraction
```

---

# 41. Frontend Review Checklist

Before completing a frontend story:

- [ ] Persian text is correct
- [ ] RTL is correct
- [ ] Safe areas are respected
- [ ] The page is responsive and was implemented mobile first
- [ ] Mobile, tablet, and desktop layouts were verified
- [ ] No unintended horizontal overflow exists
- [ ] Typography uses tokens
- [ ] Spacing uses tokens
- [ ] Icons match design-system sizing
- [ ] No random hex values
- [ ] No accidental font sizes
- [ ] Loading state exists
- [ ] Empty state exists where relevant
- [ ] Error state exists
- [ ] Touch targets are appropriate
- [ ] Bottom navigation does not cover content
- [ ] API failures are handled
- [ ] Shared components are reused
- [ ] No unnecessary duplicate CSS exists

---

# 42. Definition of Done

A frontend feature is complete when the following are addressed where applicable:

```text
Design
+
Reusable Components
+
RTL
+
Mobile Layout
+
Responsive Layout
+
API Integration
+
Validation
+
Loading State
+
Empty State
+
Error State
+
Accessibility
+
Tests
```

---

# 43. Consequences

## Positive

This decision gives the project:

- Strong design consistency
- Faster feature delivery
- Better Codex-generated code quality
- Less CSS duplication
- Easier redesign
- Easier RTL maintenance
- Better WebView behavior
- Predictable component APIs
- Clear component ownership

## Trade-offs

- Initial design-system work takes more time.
- Contributors must resist page-specific shortcuts.
- Tailwind and CSS tokens must remain aligned.
- Shared components require deliberate APIs.

These costs are accepted because the application contains many screens that must share one consistent visual system.

---

# 44. Final Rule

The frontend must not become:

> A collection of Vue pages styled independently.

It must be:

> **A feature-oriented Vue application built on a reusable, tokenized, RTL-first design system.**

When in doubt, prefer:

```text
shared token
→ reusable UI primitive
→ feature component
→ page composition
```

over duplicated page-specific code.
