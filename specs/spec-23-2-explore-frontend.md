# SPEC-23 — Private Gallery Frontend

## Objective

- Implement high-performance customer gallery UX.

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope

- Nothing

## Frontend Scope

# Implement Explore / Portfolio UI

Implement only the **frontend UI** for the Afraz Studio **Explore / Portfolio** page.

Use the provided reference images as the exact visual reference:

- Final Explore reference: `docs\design\explore\explore.png`
- Skeleton loading reference: `docs\design\explore\skeleton.png`

The final implementation should visually match the provided screenshots as closely as possible.

---

## Important

This task is **UI only**.

Do NOT implement:

- Backend API changes
- Database changes
- Real server pagination
- Real image search
- Real category filtering
- Real sorting persistence
- Analytics
- Authentication changes

Use mock/local frontend data and simulated loading behavior only.

---

## Reuse Existing Design System

Before coding, inspect the existing frontend implementation.

Reuse the exact same shared:

- `AppHeader`
- typography
- Persian font
- icon system
- spacing tokens
- color tokens
- radius tokens
- safe-area utilities
- bottom navigation
- input components
- buttons
- skeleton primitives
- modal/viewer components if already available

Do NOT create duplicate visual primitives if equivalent shared components already exist.

The Explore page must feel like it belongs to the same app as Home, Booking, Login and Profile.

---

## General Page Requirements

- Vue 3
- TypeScript
- Tailwind CSS
- Persian / RTL
- Mobile-first
- Optimized for iPhone 16/17 Pro Max
- Respect Dynamic Island and iOS safe areas
- Use the existing Persian Sans Serif font
- Use current Afraz Studio design tokens
- Maintain the existing minimal/Instagram-inspired visual language

The page should be visually very close to the reference images.

---

## App Header

Reuse the existing shared `AppHeader`.

Header should include:

- Afraz Studio logo on the right
- Title: `کشف و اکتشاف`
- Subtitle: `عکس‌های زیبا را کشف کنید ✨`
- Existing notification icon
- Existing message/send icon

Keep icon size, typography and spacing consistent with Home.

Do not create a new header implementation.

---

## Search + Sort Row

Below the header, create one horizontal row containing:

### Search

A reusable search input with:

- search icon
- placeholder: `جستجو در عکس‌ها...`
- RTL alignment
- existing input height/radius
- same visual style as reference

Search can filter local mock items only if convenient.

No API required.

### Sort

Place the sort control **beside the search field in the same row**.

Label: `مرتب‌سازی`

Use the existing icon/button style.

For UI-only behavior, opening a simple local dropdown/bottom sheet is enough.

Suggested mock options:

- جدیدترین
- قدیمی‌ترین
- محبوب‌ترین

Reuse an existing `AppBottomSheet`, `AppSelect`, or shared menu if available.

---

## Category Filters

Below search/sort, show horizontally scrollable categories exactly like the reference.

Categories:

- همه
- نوزاد
- کودک
- تولد
- بارداری
- خانوادگی
- فضای باز

Requirements:

- Instagram-like outlined black icons
- selected category uses the existing primary teal color
- selected icon becomes visually filled/emphasized
- reuse existing category icon components if already available
- horizontal scrolling when width is insufficient
- no wrapping

Use local state for selected category.

---

## Explore Photo Grid

Display photos in a **3-column Instagram Explore-style grid**.

Important visual rules:

- `border-radius: 0`
- No rounded corners on any grid photo
- Very small consistent gaps only
- Image should completely fill each grid cell
- Use `object-fit: cover`
- Keep a visually consistent tile ratio
- Do not place text/captions inside grid tiles

Use mock child/newborn/family/maternity photography assets.

Example:

```text
[ photo ][ photo ][ photo ]
[ photo ][ photo ][ photo ]
[ photo ][ photo ][ photo ]
...
```

---

## Infinite Scroll

The Explore grid must support **frontend simulated infinite scroll**.

For this story:

- Start with a local mock list
- Load an initial batch
- When the user approaches the bottom, append another mock batch
- Continue until mock data is exhausted
- Do not call any backend

Prefer using:

- `IntersectionObserver`
- a sentinel element at the end of the grid

Avoid scroll-event polling unless necessary.

Suggested behavior:

```text
Initial load
   ↓
Render 15–18 photos
   ↓
User scrolls near bottom
   ↓
Show skeleton batch
   ↓
Append next batch
```

---

## Skeleton Loading

The page must support the skeleton state exactly like the provided skeleton reference.

Requirements:

- Same 3-column grid
- Same exact tile dimensions as loaded image grid
- No radius on skeleton grid tiles
- Neutral soft-gray surfaces
- Subtle shimmer/pulse animation
- Skeleton should not change layout when replaced with real image

Create/reuse a component such as:

```text
ExploreGridSkeleton
```

or reuse the existing shared `AppSkeleton`.

Support two loading situations:

### Initial Loading

Before first batch is shown:

- Header remains visible
- Search/sort remains visible
- Categories remain visible
- Entire visible grid uses skeleton items

### Infinite Scroll Loading

When loading the next batch:

- Existing images remain visible
- Add one or more rows of skeleton tiles at the bottom
- Replace skeletons with new images after simulated delay

---

## Loading State

Maintain explicit UI state such as:

```ts
isInitialLoading
isLoadingMore
hasMore
```

No backend is required.

A small local simulated delay is acceptable for this UI-only story.

Do not introduce complex data libraries just for mock loading.

---

## Image Interaction

Each loaded grid photo should be clickable.

If the existing full-screen photo viewer/modal from the app already exists, reuse it.

Expected viewer behavior:

- fullscreen modal
- background content blurred
- selected image keeps its intended size/aspect
- image border radius = 0
- previous/next controls
- swipe/slider behavior
- close action
- current position indicator

Do NOT build a duplicate viewer if the project already contains this component.

If the viewer is not part of this story yet, clicking may use a placeholder handler, but structure the grid item API so it can connect to the shared viewer later.

---

## Bottom Navigation

Reuse the exact existing shared floating bottom navigation.

Do not create a new nav.

The navigation must:

- match Home exactly
- keep shared icon scale
- use outline icons when inactive
- filled icon when selected
- respect bottom safe area
- remain floating/liquid-glass if that is the existing implementation

Use the appropriate active tab based on the project's existing navigation structure.

---

## Suggested Component Structure

```text
features/explore/
├── pages/
│   └── ExplorePage.vue
├── components/
│   ├── ExploreToolbar.vue
│   ├── ExploreCategories.vue
│   ├── ExploreGrid.vue
│   ├── ExploreGridItem.vue
│   └── ExploreGridSkeleton.vue
├── composables/
│   └── useExploreInfiniteScroll.ts
├── types/
│   └── explore.types.ts
└── data/
    └── explore.mock.ts
```

Adapt this to the current project architecture.

Do not create unnecessary abstractions.

---

## Local Mock Data

Create enough mock entries to demonstrate real scrolling.

Suggested fields:

```ts
interface ExplorePhoto {
  id: string
  src: string
  category: ExploreCategory
  createdAt: string
  popularity?: number
}
```

Use only frontend mock data.

---

## Empty State

If local search/category filtering returns no items, display a shared empty state.

Suggested copy:

`عکسی پیدا نشد`

`فیلتر یا عبارت جستجو را تغییر دهید.`

Reuse existing `AppEmptyState` if available.

---

## Accessibility

- Search input must have accessible label
- Sort action must have accessible label
- Category buttons must expose selected state
- Grid images must use appropriate alt text
- Skeletons should not be announced as meaningful content
- Touch targets must remain mobile-friendly

---

## Performance Rules

Even though this is mock data:

- lazy-load real images
- use `loading="lazy"` where appropriate
- avoid rendering an unnecessarily huge mock list at once
- use infinite append behavior
- use `IntersectionObserver`
- keep component re-renders small

Do not add virtualization unless it is already available in the project.

---

## Scope Restrictions

Do NOT implement:

- ASP.NET Core changes
- real Explore API
- real pagination
- CDN integration
- MinIO integration
- database queries
- server-side filtering
- server-side sorting
- real analytics

This story is strictly:

```text
Explore UI
+
Search UI
+
Sort UI
+
Category UI
+
3-column image grid
+
Infinite scroll behavior
+
Skeleton/loading states
```

---

## Final Validation

After implementation:

- run frontend build
- run TypeScript type-check
- verify RTL
- verify iPhone 16/17 Pro Max layout
- verify Dynamic Island/safe-area handling
- verify search and sort stay on the same row
- verify categories horizontally scroll
- verify photo grid has exactly 3 columns
- verify all grid photos have `border-radius: 0`
- verify initial skeleton matches real grid dimensions
- verify infinite-scroll skeleton appears only at the bottom
- verify next mock batch appends without layout jump
- verify shared bottom nav is reused
- verify shared header is reused
- verify no duplicate design-system components were created

At the end briefly report:

- reused shared components
- new reusable components added
- how infinite scroll is simulated
- how skeleton loading is handled
- assumptions made
- Grid, fullscreen viewer, swipe, zoom, favorite, multi-select, selected count, filters.
- Use thumbnail-first loading and high-res on demand.
- Virtualize if gallery size requires it.


## Acceptance Criteria

- [x] Smooth on large galleries.
- [x] Selection state is robust across pagination.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
