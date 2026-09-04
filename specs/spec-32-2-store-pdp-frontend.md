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


# Implement Store PDP UI

Implement only the **frontend UI** for the Afraz Studio **Product Details Page (PDP)**.

Use the provided PDP reference images as the exact visual reference:

- `docs\design\shop\pdp\pdp-1.png`
- `docs\design\shop\pdp\pdp-2.PNG`
- `docs\design\shop\pdp\pdp-3.PNG`

The final result must visually match the screenshots as closely as possible and must reuse the existing Afraz Studio Design System.

## Scope

UI only.

Do NOT implement:

- Backend/API calls
- Database changes
- Real cart persistence
- Payment
- Real reviews API
- Real favorites persistence
- Real product inventory
- Authentication changes

Use local/mock data only.

## Stack

Use the existing project stack:

- Vue 3
- TypeScript
- Tailwind CSS
- Existing shared Design System
- Existing Persian Sans Serif font
- Existing RTL/mobile conventions

## Reuse Existing Components

Before coding, inspect the current frontend and reuse existing shared components wherever possible:

- `AppHeader`
- `AppButton`
- `AppIcon`
- `AppBadge`
- `AppCard`
- `AppTabs`
- `AppDivider`
- `AppSkeleton`
- carousel/gallery components
- typography, spacing, color and radius tokens
- safe-area / Dynamic Island utilities

Do not create duplicate Design System components.

## Header

Match the references exactly:

- Back button on the **top-right**
- Cart icon on the **top-left**
- Share icon beside cart
- Cart badge with mock item count
- Respect Dynamic Island and iOS safe areas

Reuse shared icon/button components.

## Product Gallery

Create the large product image gallery shown in the reference:

- main image
- image counter
- pagination dots
- swipe/slider behavior using local images
- consistent aspect ratio
- use existing carousel component if available

## Product Summary

Display:

- category label
- product title: `آلبوم پارچه‌ای کلاسیک`
- short subtitle
- favorite action

Do **not** show the price in the upper product-summary section.

## Tabs

Implement the three PDP tabs shown in the references:

- `مشخصات`
- `بررسی محصول`
- `دیدگاه‌ها`

Use local tab state only.

### مشخصات

Show:

- product introduction
- specifications table
- terms & conditions
- report incorrect price
- report product specifications
- Q&A entry

Keep layout and typography close to the reference.

### بررسی محصول

Show the simpler editorial overview:

- product hero/title block
- `معرفی کالا`
- `بررسی تخصصی`
- key benefits/features
- product specifications/features
- service/shipping benefits
- customer-review teaser

Reuse shared cards and icons.

### دیدگاه‌ها

Show:

- rating summary
- rating distribution bars
- user-uploaded image thumbnails
- reviews summary card
- review cards
- buyer badge
- star rating
- like/dislike controls
- `مشاهده همه دیدگاه‌ها`

Use mock review data only.

## Sticky Bottom Purchase Bar

Create the fixed bottom purchase bar exactly like the references.

Display:

- price: `۸۹۰,۰۰۰ تومان`
- primary CTA: `افزودن به سبد خرید`
- shopping bag/cart icon

Requirements:

- reuse shared `PrimaryButton`
- sticky/fixed at bottom
- respect bottom safe area
- visible across PDP tabs
- no real cart integration

## Local Interaction

Support only local UI interaction:

- image carousel
- tab switching
- favorite toggle
- review like/dislike toggle
- add-to-cart placeholder action
- share placeholder action

## Responsive Target

- Persian / RTL
- iPhone 16/17 Pro Max
- Dynamic Island safe area
- bottom safe area
- no horizontal overflow

## Suggested Structure

```text
features/store/
├── pages/
│   └── ProductDetailsPage.vue
├── components/
│   ├── ProductGallery.vue
│   ├── ProductSummary.vue
│   ├── ProductTabs.vue
│   ├── ProductSpecifications.vue
│   ├── ProductReviewOverview.vue
│   ├── ProductReviews.vue
│   └── ProductPurchaseBar.vue
└── data/
    └── product-details.mock.ts
```

Adapt to the existing project architecture and avoid unnecessary abstractions.

## Final Validation

After implementation:

- run frontend build
- run TypeScript type-check
- verify RTL
- verify header actions match the reference
- verify back is on the right and cart/share are on the left
- verify no upper price is shown
- verify all three tabs
- verify gallery interaction
- verify fixed bottom price + add-to-cart bar
- verify shared Design System components are reused
- verify no unnecessary duplicate components were introduced

At the end briefly report reused components and newly added reusable components.
