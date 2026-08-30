# Home Page Redesign Prompt — Afraz App

Reference image location: `docs\design\homepage\home-2.png`

## Task

You are modifying an EXISTING frontend application.

The current Home page implementation is NOT visually correct.

Your task is NOT to redesign, improve, reinterpret, modernize, or simplify the current UI.

Your task is to REBUILD / CORRECT the current Home screen until it visually matches the supplied reference image as closely as technically possible.

The attached reference image is the ABSOLUTE VISUAL SOURCE OF TRUTH.

The reference design is based on an **iPhone 16 Pro Max** viewport.

Reference image dimensions:

853 × 1844 px

Primary target device:

**iPhone 16 Pro Max**

---

## 1. NON-NEGOTIABLE RULE

I want the final frontend to look EXACTLY like the reference screenshot:

- same visual scale
- same font scale
- same content density
- same spacing
- same margins
- same image proportions
- same component heights
- same component widths
- same icon sizes
- same icon stroke thickness
- same border radii
- same section spacing
- same alignment
- same RTL behavior
- same header
- same stories row
- same Hero
- same cards
- same category row
- same album products
- same bottom navigation
- same white-space distribution

Do NOT preserve the current UI merely because it already exists.

If the current implementation conflicts with the screenshot:

**THE SCREENSHOT WINS.**

Refactor, restructure or replace existing frontend components/styles when required.

Do NOT make creative decisions.

Do NOT redesign anything.

---

## 2. MOBILE-FIRST IS MANDATORY

This application MUST be implemented MOBILE-FIRST.

Priority:

1. Mobile
2. Tablet
3. Desktop

The provided screenshot represents the PRIMARY design.

The mobile implementation must be completed and visually validated BEFORE making desktop adaptations.

Base Tailwind/CSS styles MUST represent mobile.

Example:

```html
<div class="grid grid-cols-4 md:grid-cols-6 lg:grid-cols-8">
```

Do NOT create desktop first and shrink it.

Desktop should be an adaptation of the mobile design.

---

## 3. PRIMARY DEVICE TARGET — IPHONE 16 PRO MAX

The supplied design was created for iPhone 16 Pro Max.

Use iPhone 16 Pro Max proportions as the main mobile target.

Use a CSS viewport close to:

```txt
430 × 932 CSS pixels
```

depending on browser/device chrome.

The provided screenshot is approximately a 2x rendered reference image.

Do NOT fake the design using global CSS transforms or browser zoom.

Implement the actual layout correctly.

Use the screenshot as the final visual authority.

---

## 4. REFERENCE COORDINATE SYSTEM

Reference:

```txt
width = 853px
height = 1844px
aspect ratio ≈ 0.4626
```

When visually comparing implementation screenshots, normalize them to the same aspect ratio.

Do NOT blindly divide every pixel value by 2.

Use the numeric guidance below only as a starting point.

The screenshot itself is the final authority.

---

## 5. PAGE BACKGROUND

Use approximately:

```css
#FCFCFB
```

or:

```css
#FDFCFB
```

The design is:

- bright
- warm white
- clean
- minimal
- not gray-heavy

Cards are separated mainly through spacing, subtle borders and very soft shadows.

---

## 6. MAIN MOBILE CONTAINER

Use approximately:

```css
width: 100%;
max-width: 430px;
margin-inline: auto;
background: #fcfcfb;
position: relative;
```

Do NOT stretch the mobile layout across large desktop widths.

On desktop, center the mobile-first composition or progressively adapt it.

---

## 7. GLOBAL HORIZONTAL PADDING

Main content:

```txt
10–16px mobile horizontal padding
```

Hero:

```txt
approximately 10px from screen edges
```

Some horizontal carousels intentionally extend near viewport edges.

Do NOT use excessive mobile padding such as 24–32px everywhere.

This design is relatively dense.

---

## 8. SAFE AREAS

Respect:

```css
env(safe-area-inset-top)
env(safe-area-inset-bottom)
```

Do NOT reproduce the iOS status bar as normal frontend content in production.

The reference visually includes:

- time
- cellular signal
- Wi-Fi
- battery

These belong to device chrome.

---

## 9. GLOBAL RTL

Use:

```html
<html lang="fa" dir="rtl">
```

Ensure:

```css
direction: rtl;
```

Prefer logical CSS properties:

```css
margin-inline
padding-inline
inset-inline-start
inset-inline-end
```

Do NOT solve RTL by manually reversing every individual component.

---

## 10. FONT

Use one Persian sans-serif font consistently.

Preferred:

```txt
Vazirmatn
```

Alternative:

```txt
IRANSansX
Dana
```

Suggested stack:

```css
font-family:
  "Vazirmatn",
  "IRANSansX",
  "Dana",
  -apple-system,
  BlinkMacSystemFont,
  sans-serif;
```

Do NOT use multiple unrelated font families.

---

## 11. TYPOGRAPHY SCALE

Fine-tune through screenshot comparison.

### Header Brand Title

Text:

```txt
آتلیه افراز قم
```

Approx:

```txt
font-size: 22–24px
font-weight: 700–800
line-height: 1.25
color: #101820
```

### Header Subtitle

```txt
ثبت خاطره‌های شیرین کودکی ✨
```

Approx:

```txt
font-size: 12–14px
font-weight: 400–500
color: #74777B
accent: #FFB522
```

### Story Labels

```txt
font-size: 13–15px
font-weight: 500–600
color: #111111
```

### Hero Eyebrow

```txt
ثبت لحظه‌های
```

Approx:

```txt
font-size: 28–31px
font-weight: 800
```

### Hero Main Title

```txt
شیرین کودکی
```

Approx:

```txt
font-size: 31–35px
font-weight: 800
line-height: 1.35
color: #121B24
```

### Hero Subtitle

```txt
خاطراتی که ماندگار می‌مانند...
```

Approx:

```txt
font-size: 16–18px
font-weight: 400–500
color: #5C6064
```

### Primary CTA

```txt
رزرو نوبت عکاسی
```

Approx:

```txt
font-size: 15–17px
font-weight: 600
```

### Quick Action Primary Text

Examples:

```txt
رزرو نوبت
سفارش‌های من
انتخاب عکس
ساخت آلبوم
```

Approx:

```txt
font-size: 15–17px
font-weight: 500–600
color: #171A1D
```

### Quick Action Secondary Text

Examples:

```txt
عکاسی
پیگیری سفارش‌ها
برای چاپ
شخصی‌سازی
```

Approx:

```txt
font-size: 12–14px
font-weight: 400
color: #989B9E
```

### Section Headings

Examples:

```txt
نمونه‌کارهای منتخب
دسته‌بندی‌ها
فروشگاه آلبوم
```

Approx:

```txt
font-size: 19–21px
font-weight: 700–800
color: #111820
```

### View All

```txt
مشاهده همه
```

Approx:

```txt
font-size: 13–14px
font-weight: 600
```

### Category Labels

```txt
font-size: 13–14px
font-weight: 500
```

### Bottom Nav Labels

```txt
font-size: 11–13px
font-weight: 500
```

Active:

```txt
font-weight: 600–700
```

---

## 12. ICON SYSTEM

Use monochrome Instagram/iOS-style icons.

Inactive:

- black
- outline
- no colored background
- clean SVG
- stroke width around 1.8–2px

Selected:

- filled icon
- same icon family
- dark navy/black
- not just thicker stroke

Do NOT use:

- colorful icons
- emoji
- 3D icons
- mixed icon packs
- Material filled circles

Prefer one consistent library.

Lucide is acceptable for outline icons.

If a filled version is missing, create a local SVG pair.

---

## 13. HEADER

Header structure:

Right side:

```txt
[logo] [آتلیه افراز قم]
       [✨ ثبت خاطره‌های شیرین کودکی]
```

Left side:

```txt
[Notification] [Paper-plane / Send]
```

There is also a small down-chevron near the brand/title region.

Logo approximate CSS size:

```txt
40–44px
```

Do NOT make the logo oversized.

---

## 14. HEADER ACTION ICONS

Bell icon:

```txt
24–27px
```

Paper-plane icon:

```txt
24–27px
```

No square white cards behind the icons.

They float directly on the page background.

Bell includes a small red notification dot.

Approx:

```txt
7–9px
```

Color:

```css
#FF3347
```

---

## 15. STORIES SECTION

Directly under the header.

Instagram-style horizontal stories row.

Critical design element.

Visible stories:

```txt
خانوادگی
تولد
بارداری
کودک
نوزاد
ثبت لحظه‌ها
```

---

## 16. STORY ITEM SIZE

Approx mobile:

```txt
story width: 58–68px
avatar outer diameter: 56–64px
photo inner diameter: 48–54px
gap: 15–22px
label margin-top: 7–9px
```

---

## 17. STORY BORDER

Use Instagram-like gradient ring.

Approx:

```txt
2–3px
```

Gradient:

```css
linear-gradient(
  135deg,
  #ff2d55,
  #ff375f,
  #ff9500
)
```

Inside the ring:

```txt
2–3px white separation
```

Image remains circular.

---

## 18. CREATE STORY ITEM

"ثبت لحظه‌ها" is different.

Use:

- white/light circle
- thin gray outline
- large plus icon
- no image
- no gradient

Approx:

```txt
circle: 62–68px
plus: 28–32px
```

---

## 19. HERO

Approx reference:

```txt
x ≈ 20
y ≈ 343
width ≈ 793
height ≈ 424
```

Approx CSS target:

```txt
height: 210–220px
```

Use aspect ratio close to:

```css
aspect-ratio: 1.87 / 1;
```

Border radius:

```txt
18–22px
```

Use:

```css
overflow: hidden;
```

---

## 20. HERO IMAGE

Photograph fills the Hero.

Composition:

- baby on RIGHT half
- text on LEFT half

Do NOT center the baby.

Do NOT crop the baby's face.

Potential starting point:

```css
object-fit: cover;
object-position: 65% center;
```

Tune visually.

---

## 21. HERO TEXT BLOCK

Text is visually placed on the LEFT side of the Hero.

Because the app is RTL, do NOT accidentally align the copy over the baby's face.

Text vertical center:

```txt
around 45–55% of Hero height
```

---

## 22. HERO DECORATIVE HEART

Use a small hand-drawn pink heart SVG.

Approx:

```txt
24–30px
```

Color:

```css
#FF91A5
```

Do NOT use emoji.

---

## 23. HERO CTA

Text:

```txt
رزرو نوبت عکاسی
```

Use dark navy pill.

Approx:

```txt
width: 125–140px
height: 48–52px
border-radius: 999px
```

Background:

```css
#132938
```

or:

```css
#152B39
```

Text:

```css
color: white;
```

Calendar icon:

```txt
22–24px
```

---

## 24. HERO SLIDER DOTS

Bottom-center of Hero.

4 indicators.

Active:

```txt
18 × 6px
```

Inactive:

```txt
7 × 7px
```

Gap:

```txt
7px
```

Active color:

dark navy.

Inactive:

light gray.

---

## 25. QUICK ACTIONS

Directly under Hero.

Four actions in one row.

No individual cards.

Thin vertical separators between items.

RTL order:

```txt
ساخت آلبوم
انتخاب عکس
سفارش‌های من
رزرو نوبت
```

Each:

```txt
icon
primary label
secondary label
```

Center aligned.

---

## 26. QUICK ACTION AREA

Approx mobile:

```txt
height: 110–125px
```

Each item:

```txt
width: 25%
padding: 12–18px
```

Separators:

```txt
1px
color: #ECECEC
height: 65–80px
```

---

## 27. QUICK ACTION ICONS

Approx:

```txt
28–32px
```

Stroke:

```txt
1.8–2px
```

Black.

Suggested icons:

```txt
رزرو نوبت → Calendar
سفارش‌های من → Clipboard / Document
انتخاب عکس → Image
ساخت آلبوم → Bookmark / Album
```

---

## 28. FEATURED PORTFOLIO

Heading:

```txt
نمونه‌کارهای منتخب
```

Right aligned.

Opposite action:

```txt
← مشاهده همه
```

---

## 29. PORTFOLIO CAROUSEL

Use horizontal scrolling.

Do NOT force all images into a static grid.

The reference intentionally shows a partially clipped next item.

Use:

```css
overflow-x: auto;
scroll-snap-type: x mandatory;
```

Hide scrollbar.

---

## 30. PORTFOLIO CARD SIZE

Reference shows approximately 4.5 images across the screen.

Target width:

```txt
20–22vw
```

with min/max values.

Aspect ratio roughly:

```txt
1 : 0.95
```

Border radius:

```txt
12–14px
```

Gap:

```txt
10–12px
```

---

## 31. CATEGORIES

Heading:

```txt
دسته‌بندی‌ها
```

Categories:

```txt
فضای باز
خانوادگی
بارداری
تولد
کودک
نوزاد
```

No large card backgrounds.

Use circular icon containers + labels.

---

## 32. CATEGORY ICON CONTAINERS

Approx:

```txt
52–58px diameter
```

Border:

```css
1px solid #ECECEC
```

Background:

transparent or white.

Icon:

```txt
25–30px
```

Label gap:

```txt
6–8px
```

---

## 33. PHOTO READY CARD

Title:

```txt
عکس‌های شما آماده انتخاب است
```

Horizontal card.

Approx mobile:

```txt
height: 105–120px
border-radius: 15–18px
```

Background:

```css
#FAFCFC
```

Border:

```css
1px solid #F0F1F2
```

No strong shadow.

---

## 34. PHOTO READY CARD CONTENT

RIGHT:

- thumbnail image
- small dark badge
- badge text: ۲۲۵

Badge:

```txt
dark charcoal/navy
white text
radius: 6–8px
```

Center:

```txt
عکس‌های شما آماده انتخاب است
۱۲ فروردین ۱۴۰۴ - آتلیه کودک
مشاهده و انتخاب عکس‌ها
```

Left:

subtle decorative cyan camera illustration.

Approx:

```css
#A9E8EF
```

Low opacity.

---

## 35. ALBUM STORE

Heading:

```txt
فروشگاه آلبوم
```

Horizontal carousel.

Visible products:

```txt
آلبوم لوکس
آلبوم کودک
آلبوم نوزاد
```

Cards use pastel photography.

---

## 36. ALBUM CARD

Approx:

```txt
width: 220–260px
height: 120–145px
border-radius: 15–18px
```

Text:

```txt
آلبوم لوکس
مشاهده محصولات
```

Main:

```txt
16–18px
600–700
```

Secondary:

```txt
12–13px
```

---

## 37. BOTTOM NAVIGATION

Critical component.

Floating iOS liquid-glass navigation.

Approx:

```txt
left/right: 24–28px
bottom: 20–28px + safe area
height: 72–82px
border-radius: 40–45px
```

---

## 38. LIQUID GLASS NAV

Use approximately:

```css
background: rgba(255,255,255,.82);

backdrop-filter:
  blur(26px)
  saturate(160%);

-webkit-backdrop-filter:
  blur(26px)
  saturate(160%);

border:
  1px solid rgba(255,255,255,.85);

box-shadow:
  0 10px 35px rgba(30,35,40,.08);
```

Do NOT use:

- dark navbar
- gray Material navbar
- square/rectangular dock

---

## 39. NAV ITEMS

Five items:

```txt
خانه
رزرو
سفارش‌ها
فروشگاه
پروفایل
```

Evenly distributed.

Icon size:

```txt
26–29px
```

Label:

```txt
11–13px
```

---

## 40. ACTIVE HOME STATE

Home is active.

Icon:

- filled
- dark navy
- not teal
- not outline

Approx:

```css
#142632
```

Label:

dark navy / black.

Tiny red dot:

```txt
~5px
```

Inactive:

black outline icons.

---

## 41. NAVBAR OVERLAP

Content may continue behind floating navbar.

Add bottom page padding:

```css
padding-bottom:
  calc(110px + env(safe-area-inset-bottom));
```

---

## 42. SHADOWS

Very subtle.

Example:

```css
box-shadow:
  0 8px 24px rgba(0,0,0,.035);
```

Most sections should not use noticeable shadows.

---

## 43. BORDERS

Use subtle borders:

```css
border:
  1px solid rgba(15, 23, 42, .06);
```

Avoid visible gray outlines.

---

## 44. CORNER RADII SYSTEM

Use approximately:

```txt
small image: 12px
normal card: 16px
Hero: 20px
floating nav: 40px+
pill: 9999px
```

---

## 45. CONTENT DENSITY

The reference is dense.

Do NOT oversize:

- headings
- icons
- cards
- section gaps
- margins
- vertical padding

Avoid excessive white space.

The final screen must preserve the same scale as the screenshot.

---

## 46. COMPONENT STRUCTURE

Recommended Vue structure:

```txt
HomeView.vue

components/home/
├── HomeHeader.vue
├── StoryCarousel.vue
├── StoryItem.vue
├── HomeHero.vue
├── HeroPagination.vue
├── QuickActions.vue
├── QuickActionItem.vue
├── SectionHeader.vue
├── PortfolioCarousel.vue
├── PortfolioCard.vue
├── CategoryCarousel.vue
├── CategoryItem.vue
├── PhotoReadyCard.vue
├── AlbumStoreCarousel.vue
├── AlbumCard.vue
└── BottomNavigation.vue
```

Do NOT put the whole page into one component.

---

## 47. DATA-DRIVEN LISTS

Stories:

```ts
[
  "خانوادگی",
  "تولد",
  "بارداری",
  "کودک",
  "نوزاد",
  "ثبت لحظه‌ها"
]
```

Categories:

```ts
[
  "فضای باز",
  "خانوادگی",
  "بارداری",
  "تولد",
  "کودک",
  "نوزاد"
]
```

Quick actions:

```ts
[
  {
    title: "ساخت آلبوم",
    subtitle: "شخصی‌سازی"
  },
  {
    title: "انتخاب عکس",
    subtitle: "برای چاپ"
  },
  {
    title: "سفارش‌های من",
    subtitle: "پیگیری سفارش‌ها"
  },
  {
    title: "رزرو نوبت",
    subtitle: "عکاسی"
  }
]
```

---

## 48. DO NOT USE SCREENSHOT AS UI

Mandatory.

Do NOT:

- use screenshot as page background
- slice UI controls from screenshot
- rasterize Persian text
- use screenshot fragments for icons

Only photographs and brand artwork may be raster assets.

Everything else must be real:

```txt
Vue
HTML
CSS
SVG
```

---

## 49. IMAGE ASSETS

Use exact matching photographic assets whenever available:

- logo
- story images
- Hero image
- portfolio images
- photo-ready thumbnail
- album card imagery

If matching assets already exist in the project, reuse them.

Do NOT use random placeholders when exact assets are available.

Image crop and composition are part of visual accuracy.

---

## 50. CURRENT IMPLEMENTATION

The app already contains a Home screen implementation.

Do NOT assume it is correct.

Before changes:

1. inspect current Home screen
2. inspect shared components
3. inspect typography
4. inspect Tailwind configuration
5. inspect design tokens
6. inspect image assets
7. inspect navigation
8. inspect responsive rules

Reuse only what helps reproduce the reference.

If existing components block visual accuracy:

refactor or replace them.

---

## 51. VISUAL REGRESSION LOOP

Do NOT stop after coding.

Required workflow:

```txt
Reference screenshot
        ↓
Inspect current implementation
        ↓
Implement corrections
        ↓
Run app
        ↓
Capture iPhone 16 Pro Max screenshot
        ↓
Compare with reference
        ↓
Fix visible differences
        ↓
Capture again
        ↓
Repeat
```

Continue until no major visual differences remain.

---

## 52. PRIMARY SCREENSHOT TEST

Primary target:

```txt
430 × 932 CSS px
```

Also test:

```txt
430 × 922
390 × 844
375 × 812
```

But iPhone 16 Pro Max is the highest-priority target.

---

## 53. COMPARISON PRIORITY

Fix differences in this order:

1. overall scale
2. page width
3. vertical density
4. header position
5. story size
6. story spacing
7. Hero dimensions
8. Hero crop
9. Hero typography
10. quick action height
11. portfolio item size
12. category sizing
13. photo-ready card
14. album carousel
15. bottom nav
16. font sizes
17. icon sizes
18. radii
19. colors
20. micro spacing
21. shadows

---

## 54. OVERLAY COMPARISON

For final QA:

1. render Home at iPhone 16 Pro Max viewport
2. capture screenshot
3. resize/normalize the reference
4. overlay both at approximately 50% opacity
5. compare boundaries and baselines

Check:

- header alignment
- story circles
- Hero bounds
- text position
- quick action baseline
- portfolio image edges
- categories
- ready-card geometry
- album cards
- bottom nav position

Correct all large visible discrepancies.

---

## 55. MOBILE-FIRST RESPONSIVE STRATEGY

Base styles MUST target iPhone/mobile first.

Use breakpoints only for progressive enhancement.

Example:

```html
<div class="
  grid
  grid-cols-4
  gap-3
  md:grid-cols-6
  md:gap-4
  lg:grid-cols-8
">
```

Priority:

```txt
Mobile UX and visual accuracy
        ↓
Tablet adaptation
        ↓
Desktop adaptation
```

Do NOT start desktop optimization before the mobile screenshot closely matches the reference.

---

## 56. ACCEPTANCE CRITERIA

The task is NOT complete simply because:

- app compiles
- Tailwind works
- components exist
- page is responsive

It is complete only when the rendered mobile UI is visually very close to the reference.

Checklist:

- [ ] iPhone 16 Pro Max composition matches
- [ ] overall scale matches
- [ ] page density matches
- [ ] RTL correct
- [ ] stories match Instagram-like style
- [ ] Hero dimensions match
- [ ] Hero crop matches
- [ ] Hero text is on the correct side
- [ ] CTA matches
- [ ] quick action row matches
- [ ] portfolio is horizontally scrollable
- [ ] category row matches
- [ ] photo-ready card matches
- [ ] album store matches
- [ ] bottom navigation floats over content
- [ ] liquid-glass effect matches
- [ ] active Home icon is filled
- [ ] inactive icons are outline
- [ ] icon sizes are consistent
- [ ] typography hierarchy matches
- [ ] content is not oversized
- [ ] no unnecessary gaps
- [ ] exact/approved imagery is used
- [ ] safe areas are respected
- [ ] no horizontal page overflow
- [ ] responsive behavior works
- [ ] visual screenshot comparison performed

---

## 57. MOST IMPORTANT INSTRUCTION

Do NOT return a merely similar design.

Do NOT preserve the current frontend look if it differs.

Do NOT redesign the reference.

Do NOT interpret the screenshot loosely.

I explicitly want the Home screen to match the reference exactly.

The screenshot is the visual specification.

If the existing implementation looks different:

**CHANGE THE IMPLEMENTATION.**

Keep iterating until the rendered Home page on iPhone 16 Pro Max visually matches the supplied reference as closely as possible.
