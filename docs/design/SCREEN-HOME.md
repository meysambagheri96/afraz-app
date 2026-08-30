# SCREEN-HOME — Afraz Home Screen Specification

> Screen: Home  
> Locale: Persian (`fa-IR`)  
> Direction: RTL  
> Reference: approved Afraz homepage screenshot  
> Goal: reproduce the screenshot closely; do not redesign.

## 1. Page Composition

The Home screen is a vertical scroll page with a floating bottom navigation.

```txt
HomeView
│
├── AppHeader
│   ├── BrandLogo
│   ├── BrandTitle
│   ├── BrandSubtitle
│   ├── SearchButton
│   └── NotificationButton
│
├── HeroBanner
│   ├── HeroImage
│   ├── Eyebrow
│   ├── HeroTitle
│   ├── HeroSubtitle
│   └── BookingCTA
│
├── QuickActions
│   ├── ساخت آلبوم
│   ├── انتخاب عکس برای چاپ
│   ├── سفارش‌های من
│   └── رزرو نوبت
│
├── FeaturedPortfolio
│   ├── SectionHeader
│   └── 3 portfolio images
│
├── Categories
│   ├── SectionHeader
│   └── 4 category cards
│
├── LastOrderCard
│
└── BottomNavigation
```

## 2. Header

Right side brand group:

```txt
[logo] [آتلیه افراز قم]
       [✨ ثبت خاطره‌های شیرین کودک شما]
```

Visual rules:

- Circular logo.
- Main title dark teal and bold.
- Subtitle charcoal.
- Sparkle accent in orange.
- Entire group vertically centered.

Left-side actions:

```txt
[Notification] [Search]
```

Each is a rounded white square card with black outline icon, very subtle shadow, and no strong border. Notification has a small orange dot near the bell.

Approximate spacing:

```txt
page top spacing: 24px after safe-area
horizontal padding: 28–34px on reference composition
gap between icon buttons: 20–24px
```

On narrower devices, scale horizontal padding down to 16px.

## 3. Hero Banner

Large rounded photographic banner directly below the header.

Content:

```txt
ثبت لحظه‌های
شیرین کودکی
خاطراتی که ماندگار می‌مانند...
[ رزرو نوبت  ‹ ]
```

Image asset:

```txt
hero-baby.webp
```

Composition:

- Baby occupies the right half.
- Text occupies the left half.
- Use warm cream background derived from the photo.
- Card has large rounded corners.
- No dark overlay.
- Do not bake hero copy into the image.

CTA:

- Teal pill.
- White text.
- Chevron at visual left of label.
- Approximately 56–64px high.
- Strong but not overly saturated.

## 4. Quick Actions

Four equal-width cards in one row on the reference width.

RTL visual order:

```txt
ساخت آلبوم
انتخاب عکس برای چاپ
سفارش‌های من
رزرو نوبت
```

Each card:

```txt
white surface
large radius
very subtle shadow
centered icon
centered label
```

Icons are black outline.

Suggested data model:

```ts
const quickActions = [
  { id: "album", label: "ساخت آلبوم", icon: BookHeart },
  { id: "photo-selection", label: "انتخاب عکس برای چاپ", icon: Images },
  { id: "orders", label: "سفارش‌های من", icon: Inbox },
  { id: "booking", label: "رزرو نوبت", icon: CalendarCheck }
]
```

At very narrow widths, preserve usable tap targets; reducing gaps is preferred over wrapping to two rows unless necessary.

## 5. Featured Portfolio

Section heading:

```txt
✨ نمونه‌کارهای منتخب
```

Heading aligned right. "مشاهده همه" link aligned left with chevron.

Grid:

- Three images.
- Equal visual height.
- Small rounded corners.
- Small horizontal gaps.

Assets:

```txt
portfolio-01.webp
portfolio-02.webp
portfolio-03.webp
```

Use:

```css
grid-template-columns: repeat(3, minmax(0, 1fr));
```

Images must use `object-fit: cover`.

## 6. Categories

Section heading:

```txt
دسته‌بندی‌ها
```

Left action:

```txt
مشاهده همه
```

Four cards in a row.

RTL visual order:

```txt
نوزاد
کودک
تولد
خانوادگی
```

Icons:

```txt
Baby
Child
Cake
Users
```

All icons are black outline with consistent stroke and no color fills.

Cards are shorter and more compact than Quick Action cards.

## 7. Last Order Card

Large horizontal card near the bottom.

Title:

```txt
آخرین سفارش شما
```

The card contains three logical areas.

Thumbnail:

```txt
order-thumbnail.webp
```

Rounded square portrait.

Order identity/status:

```txt
آتلیه نوزاد
۱۲ فروردین ۱۴۰۴
در حال ویرایش
```

"در حال ویرایش" is displayed as a soft orange pill.

Photo selection summary:

```txt
۲۳۵
عکس جدید
مشاهده و انتخاب
```

Include a small photo-stack outline icon.

The teal action text should feel clickable but not look like a heavy button.

## 8. Bottom Navigation

Fixed/floating at the bottom of the app shell.

Items in RTL visual order:

```txt
خانه
رزرو
سفارش‌ها
فروشگاه
پروفایل
```

Current active item:

```txt
خانه
```

Active Home state:

- filled Home icon,
- teal/dark-teal icon,
- teal/dark-teal label,
- subtle translucent active area.

Inactive state:

- black outline icon,
- dark label,
- no colored icon.

Container:

- white translucent liquid-glass surface,
- strong blur,
- large rounded corners,
- subtle top/border highlight,
- soft shadow.

Leave room below for the iOS Home indicator/safe area.

## 9. Page Background

Use warm off-white rather than harsh pure white:

```css
background: #FCFBF9;
```

Section cards remain white, creating a very subtle depth difference.

## 10. Spacing Reference

Approximate values:

```txt
page horizontal padding: 16px responsive
header → hero: 24px
hero → quick actions: 28–32px
quick actions → portfolio: 34–40px
portfolio title → photos: 12px
portfolio → categories: 26–32px
categories title → cards: 12px
categories → last order: 24–30px
last order → bottom nav: 16–24px
```

Tune final values through screenshot comparison.

## 11. Home Screen Data Contract

```ts
interface QuickAction {
  id: string
  label: string
  route: string
  icon: Component
}

interface PortfolioItem {
  id: string
  imageUrl: string
  alt: string
}

interface CategoryItem {
  id: string
  label: string
  route: string
  icon: Component
}

interface LastOrder {
  id: string
  studioName: string
  dateLabel: string
  status: "editing" | "ready" | "completed"
  newPhotoCount: number
  thumbnailUrl: string
}
```

Keep displayed data separate from visual components.

## 12. Suggested Vue Structure

```vue
<template>
  <div class="home-page">
    <AppHeader />

    <main>
      <HeroBanner />
      <QuickActions />
      <FeaturedPortfolio />
      <CategoryGrid />
      <LastOrderCard />
    </main>

    <BottomNavigation active="home" />
  </div>
</template>
```

`HomeView.vue` should compose components, not contain all card markup directly.

## 13. Asset Mapping

```txt
logo.png
→ AppHeader

hero-baby.webp
→ HeroBanner

portfolio-01.webp
portfolio-02.webp
portfolio-03.webp
→ FeaturedPortfolio

order-thumbnail.webp
→ LastOrderCard
```

## 14. Functional Navigation

Expected interactions:

```txt
Search icon → Search screen
Notification icon → Notifications screen
رزرو نوبت → Booking flow
سفارش‌های من → Orders screen
انتخاب عکس برای چاپ → Photo selection flow
ساخت آلبوم → Album creation flow
Portfolio "مشاهده همه" → Portfolio screen
Category card → Filtered portfolio/category screen
Last order "مشاهده و انتخاب" → Latest order/photo selection
Bottom navigation → corresponding top-level route
```

## 15. Visual Constraints

Codex must NOT:

- redesign the hero,
- reorder sections,
- introduce gradients not visible in the reference,
- add colored icons,
- add large borders,
- use Material UI styling,
- replace Persian copy with English,
- bake copy/buttons into images,
- make the navbar opaque gray,
- use outline Home icon for the active state.

Codex SHOULD:

- preserve generous white space,
- keep shadows extremely soft,
- use RTL natively,
- match image crop carefully,
- maintain the warm premium photography look,
- use reusable components.

## 16. Screenshot Validation Checklist

Before considering Home complete, compare against the supplied reference and verify:

- [ ] Header vertical position matches.
- [ ] Logo size matches.
- [ ] Search/Bell card sizes match.
- [ ] Hero aspect ratio and crop match.
- [ ] Hero copy alignment matches.
- [ ] Hero CTA size matches.
- [ ] Four quick-action cards have equal sizing.
- [ ] Portfolio grid has three equal visual columns.
- [ ] Category row has four equal cards.
- [ ] Last-order card content is aligned correctly in RTL.
- [ ] Bottom navigation is floating and translucent.
- [ ] Home icon is visually filled/selected.
- [ ] All inactive icons are black outline.
- [ ] Typography hierarchy is close to reference.
- [ ] No horizontal overflow exists.
- [ ] iOS safe areas are respected.

## 17. Implementation Priority

Implement in this order:

```txt
1. Page shell + RTL + font
2. Header
3. Hero
4. Quick actions
5. Portfolio
6. Categories
7. Last order
8. Bottom navigation
9. Responsive adjustments
10. Screenshot comparison and visual correction
```

The screenshot is the final visual authority for this screen.
