# SPEC-32 — Online Store Frontend

## Objective

- Implement premium photography-product shopping PLP UI.

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope

- No backend implementation is required in this story unless needed to support the frontend contract.

## Frontend Scope

# Implement Store PLP UI

Implement only the **frontend UI** for the Afraz Studio **Store / Product Listing Page (PLP)**.

Use the existing Afraz Studio screens and provided store references as the visual source of truth:

- `docs\design\shop\plp.png`

The final result must match the existing Afraz app Design System exactly.

## Scope

UI only.

Do NOT implement:

- Backend/API calls
- Database changes
- Real cart persistence
- Payment
- Real product search/filter API
- Authentication changes

Use local/mock product data only.

## Stack

Use the existing project stack:

- Vue 3
- TypeScript
- Tailwind CSS
- Existing shared Design System
- Existing Persian Sans Serif font
- Existing RTL/mobile conventions

## Design System Reuse

Before coding, inspect the existing app and reuse shared components wherever possible:

- `AppHeader`
- `BottomNavigation`
- `AppInput`
- `AppButton`
- `AppIcon`
- `AppBadge`
- `AppSkeleton`
- cards/surfaces
- spacing, colors, typography and radius tokens
- safe-area / Dynamic Island handling

Do not duplicate existing components.

## Page Layout

Create the Store PLP page in Persian / RTL.

Header:

- reuse existing app header
- title: `فروشگاه`
- subtitle: `محصولات آتلیه افراز`
- keep existing notification/message/header behavior if already shared

Below header:

### Search + Sort

Place in one row:

- search input: `جستجو در محصولات...`
- sort control: `مرتب‌سازی`

Mock sort options:

- جدیدترین
- پرفروش‌ترین
- ارزان‌ترین
- گران‌ترین

### Categories

Add horizontally scrollable category chips/tabs, for example:

- همه
- آلبوم نوزاد
- آلبوم کودک
- آلبوم لوکس
- قاب عکس
- چاپ عکس
- سایر

Use local state only.

## Product Grid

Display products as a clean **2-column mobile grid**.

Each Product Card should include only useful PLP information:

- product image
- product title
- short variant/size label if needed
- price
- optional old price / discount badge
- availability badge if needed
- favorite icon
- tap/click target for future PDP navigation

Keep the cards minimal and consistent with the Afraz Home/Explore visual language.

Use the existing warm, child-photography palette and typography.

## Product Card Rules

- image should be visually dominant
- use consistent image aspect ratio
- use existing shared radius tokens for product cards/images
- do not overfill cards with metadata
- Persian price formatting
- use `تومان`
- use existing outlined icon style
- selected/favorite state may be local only

## Loading / Skeleton

Support PLP loading state.

Requirements:

- same 2-column layout
- skeleton cards must match real card dimensions
- skeleton image + title + price placeholders
- subtle shimmer/pulse
- no layout shift

Use mock loading state only.

## Empty State

If local filter/search has no products:

`محصولی پیدا نشد`

`فیلتر یا عبارت جستجو را تغییر دهید.`

Reuse shared empty-state component if available.

## Interaction

Local/mock interaction only:

- category selection
- search filtering
- sort selection
- favorite toggle
- product click can route to a placeholder/existing PDP route if already defined

No real backend.

## Bottom Navigation

Reuse the exact shared floating/liquid-glass bottom navigation.

The `فروشگاه` item must be the active item.

Do not create a new bottom nav.

## Responsive Target

- iPhone 16/17 Pro Max
- RTL
- Dynamic Island safe area
- bottom safe area
- no horizontal overflow

## Suggested Structure

```text
features/store/
├── pages/
│   └── ProductListPage.vue
├── components/
│   ├── StoreToolbar.vue
│   ├── ProductCategoryTabs.vue
│   ├── ProductGrid.vue
│   ├── ProductCard.vue
│   └── ProductCardSkeleton.vue
└── data/
    └── products.mock.ts
```

Adapt to the existing architecture and avoid unnecessary abstractions.

## Final Validation

After implementation:

- run frontend build
- run TypeScript type-check
- verify RTL
- verify iPhone layout
- verify shared header/nav/components are reused
- verify 2-column PLP grid
- verify search/sort are in one row
- verify categories scroll horizontally
- verify loading skeleton
- verify no duplicate Design System components were added

At the end briefly report reused components and newly added reusable components.
