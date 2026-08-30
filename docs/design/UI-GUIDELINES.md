# Afraz UI Implementation Guidelines

> These rules are intended for Codex and frontend developers implementing the approved Afraz UI.

## 1. Frontend Stack

Use:

```txt
Vue 3
TypeScript
Vite
Tailwind CSS
Vue Router
Pinia only when shared state is actually required
Lucide icons or one equivalent consistent icon set
```

Prefer Composition API and `<script setup lang="ts">`.

## 2. RTL Is Mandatory

Set RTL at the application root:

```html
<html lang="fa" dir="rtl">
```

Do not solve RTL by manually reversing every individual component.

Use logical CSS properties where possible:

```css
margin-inline-start
margin-inline-end
padding-inline
inset-inline-start
```

Avoid `left/right` when a logical property can express the same intent.

## 3. Componentization

Do not implement the Home screen as one large Vue component.

Recommended structure:

```txt
src/
├── components/
│   ├── app/
│   │   ├── AppHeader.vue
│   │   ├── BottomNavigation.vue
│   │   └── SectionHeader.vue
│   ├── home/
│   │   ├── HeroBanner.vue
│   │   ├── QuickActions.vue
│   │   ├── QuickActionCard.vue
│   │   ├── FeaturedPortfolio.vue
│   │   ├── CategoryGrid.vue
│   │   ├── CategoryCard.vue
│   │   └── LastOrderCard.vue
│   └── ui/
│       ├── IconButton.vue
│       ├── AppButton.vue
│       └── AppCard.vue
├── views/
│   └── HomeView.vue
├── assets/
│   └── images/
└── styles/
    └── tokens.css
```

A component is justified when it repeats, has its own interaction, has a clear visual responsibility, or benefits from isolated testing.

## 4. Data-Driven Repeated UI

Do not manually duplicate four quick-action cards or category cards.

```ts
const quickActions = [
  { id: "booking", label: "رزرو نوبت", icon: CalendarCheck },
  { id: "orders", label: "سفارش‌های من", icon: Inbox },
  { id: "photos", label: "انتخاب عکس برای چاپ", icon: Images },
  { id: "album", label: "ساخت آلبوم", icon: BookHeart }
]
```

Render using `v-for` with stable keys.

## 5. No Screenshot-as-UI

Never:

- use the screenshot as a page background,
- crop UI controls from the screenshot,
- use rasterized text,
- use screenshot fragments for icons.

Only photographic content and brand logo should be image assets.

Everything else must be real HTML/CSS/SVG.

## 6. Responsive Strategy

The supplied screenshot is the primary mobile reference.

Implementation requirements:

```txt
320px–430px phones: fully supported
larger mobile widths: preserve proportions
tablet: center content with max-width
desktop web preview: center mobile canvas rather than stretching
```

Suggested shell:

```html
<main class="mx-auto min-h-dvh w-full max-w-[480px]">
```

Do not lock the app to a fixed pixel width.

## 7. Safe Areas

Use safe-area insets for iOS:

```css
.app-shell {
  padding-top: max(12px, env(safe-area-inset-top));
  padding-bottom: max(12px, env(safe-area-inset-bottom));
}
```

Bottom navigation must not collide with the Home indicator.

## 8. Dynamic Island / Device Chrome

If this is a web/PWA implementation:

- Do not draw fake iOS status-bar elements in production.
- Let the device/browser render actual system UI.
- A Dynamic Island mock may only exist inside a dedicated preview/demo frame.

If the product requirement explicitly uses an in-app device mockup, isolate it into a preview-only component.

## 9. Icons

Use SVG component icons.

Rules:

```txt
inactive: outline
selected nav item: filled
default icon color: near-black
active icon/label: teal/dark teal
stroke widths must remain consistent
```

Do not mix Lucide, Font Awesome, emoji and raster icons in one interface.

For a missing filled variant, create a local SVG pair:

```txt
HomeOutlineIcon.vue
HomeFilledIcon.vue
```

## 10. Image Handling

Assets:

```txt
src/assets/images/logo.png
src/assets/images/hero-baby.webp
src/assets/images/portfolio-01.webp
src/assets/images/portfolio-02.webp
src/assets/images/portfolio-03.webp
src/assets/images/order-thumbnail.webp
```

Use WebP where transparency is not required.

Add explicit width/height or aspect ratio to prevent layout shift.

## 11. Typography

Load one Persian font globally.

Do not apply different font families per component.

Use semantic hierarchy:

```txt
page/header brand: 24–28px
hero main heading: 34–38px
section heading: 20–22px
card labels: 15–17px
metadata: 12–14px
```

Exact final values should be tuned through screenshot comparison.

## 12. CSS and Tailwind Rules

Prefer Tailwind classes for layout and component styling.

Use CSS variables for global tokens.

Avoid:

```txt
!important
deep selector chains
large inline style objects
magic z-index values
duplicated arbitrary colors
```

Use arbitrary Tailwind values only for pixel matching when no design token fits.

## 13. Interaction States

Every interactive component must implement:

```txt
default
hover (desktop)
active/pressed
focus-visible
disabled when applicable
```

Pressed state should be subtle:

```css
transform: scale(.98);
```

Avoid excessive animation.

## 14. Motion

Use restrained motion:

```txt
150–220ms
ease-out
opacity / transform
```

Respect `prefers-reduced-motion`.

## 15. Bottom Navigation Behavior

Bottom nav remains visually available while browsing top-level app routes.

Use route state to select the active item.

```ts
const isActive = route.name === item.routeName
```

Selected state:

- filled icon,
- stronger label,
- subtle translucent background/highlight.

## 16. Semantic HTML

Use semantic elements such as `<header>`, `<nav>`, `<main>`, `<section>`, and `<button>`.

Do not make clickable `<div>` elements when a button or link is appropriate.

## 17. Accessibility

Requirements:

- `aria-label` on Search and Notifications.
- visible focus states for keyboard navigation.
- meaningful `alt` text.
- minimum 44px touch targets.
- headings in correct hierarchy.
- navigation announced as navigation.

## 18. Visual QA Workflow

Codex must not stop after the first implementation.

```txt
reference screenshot
        ↓
implementation
        ↓
run app
        ↓
capture screenshot
        ↓
compare visually
        ↓
fix spacing / sizing / font scale / alignment / image crop / radii / shadows
        ↓
repeat
```

Priority:

1. Overall layout/proportions.
2. Section spacing.
3. Typography.
4. Image framing.
5. Component size.
6. Icons.
7. Colors.
8. Shadows and micro-details.

## 19. Definition of Done

The Home UI is done only when:

- RTL is correct.
- No screenshot fragments are used as UI.
- All repeated blocks are componentized.
- Images are real assets.
- Icons use outline/filled states correctly.
- Bottom navigation matches the approved liquid-glass treatment.
- Layout works from 320px to 430px.
- Safe areas are handled.
- Visual comparison against the reference has been performed.
- There are no obvious spacing, clipping or overflow differences.
