# Afraz App Design System

> Target implementation: Vue 3 + TypeScript + Tailwind CSS.
> Primary reference: the approved Afraz mobile home screen image.
> The reference image is the visual source of truth when a numeric value in this document differs slightly from the screenshot.

## 1. Design Principles

- Mobile-first.
- Entire application is Persian and RTL.
- Clean iOS-inspired visual language.
- Warm photography + neutral white UI.
- Minimal color usage.
- Primary icons are monochrome black outline.
- Selected navigation icons become filled.
- Cards use large corner radii, soft borders and very subtle shadows.
- Avoid colorful/3D icons inside the UI.
- UI text, icons and buttons must be real frontend elements; never bake them into screenshots.

## 2. Layout

### Base viewport

Use the approved mobile composition as the visual target.

```txt
direction: rtl
background: warm white
content width: 100%
horizontal page padding: 16px
section gap: 28–36px
```

Respect iOS safe areas:

```css
padding-top: env(safe-area-inset-top);
padding-bottom: env(safe-area-inset-bottom);
```

Do not hardcode the whole screen to a single device width. Preserve the same proportions responsively.

## 3. Color Tokens

Use CSS variables as the single source of truth.

```css
:root {
  --color-bg: #FCFBF9;
  --color-surface: #FFFFFF;
  --color-surface-soft: #F8FAFB;

  --color-primary: #075D69;
  --color-primary-strong: #03454F;
  --color-primary-soft: #E8F5F5;

  --color-text: #172B2F;
  --color-text-strong: #075D69;
  --color-text-muted: #66777A;

  --color-border: rgba(23, 43, 47, 0.08);
  --color-icon: #1D1F21;

  --color-accent-pink: #FF6B8A;
  --color-accent-yellow: #FFC857;
  --color-accent-mint: #7DD3C7;
  --color-accent-blue: #8FBAFB;
  --color-accent-lilac: #C9B8E8;

  --color-white: #FFFFFF;
  --color-black: #111111;
}
```

Rules:

- Teal is reserved for important headings, links, CTA buttons and active states.
- Primary body text is near-black/charcoal.
- Pink, yellow, mint, blue and lilac are controlled accents for status, illustration and child-friendly details.
- Accent colors must remain secondary to photography, neutral surfaces and the teal brand color.
- Do not introduce extra brand colors without a design requirement.

## 4. Typography

Use a modern Persian sans-serif font.

Preferred order:

```css
font-family:
  "Vazirmatn",
  "IRANSansX",
  "Dana",
  system-ui,
  -apple-system,
  sans-serif;
```

Recommended type scale:

| Token | Size | Weight | Usage |
|---|---:|---:|---|
| `text-xs` | 12px | 400–500 | metadata |
| `text-sm` | 14px | 400–500 | secondary labels |
| `text-base` | 16px | 400–500 | card labels |
| `text-lg` | 18px | 500–600 | section links / important labels |
| `text-xl` | 20px | 600–700 | section titles |
| `text-2xl` | 24px | 700 | header title |
| `text-hero` | 34–38px | 700 | hero title |

Guidelines:

- Persian copy must never appear artificially bold.
- Use comfortable line-height: `1.5–1.8`.
- Keep numerals visually aligned with adjacent Persian text.
- Use RTL alignment except where the visual reference clearly requires centering.

## 5. Spacing Scale

```txt
4px   xs
8px   sm
12px  md
16px  base
20px  lg
24px  xl
32px  2xl
40px  3xl
48px  4xl
```

Use the scale consistently. Avoid arbitrary spacing unless required for pixel matching.

## 6. Radius Tokens

```css
--radius-sm: 12px;
--radius-md: 18px;
--radius-lg: 24px;
--radius-xl: 30px;
--radius-pill: 9999px;
```

Usage:

- Header icon buttons: `20–24px`.
- Hero banner: `24–28px`.
- Quick action cards: `28–32px`.
- Portfolio images: `8–12px`.
- Category cards: `18–22px`.
- Last order container: `24–28px`.
- Bottom navigation: `32–40px`.

## 7. Shadows

Shadows must be soft and low-contrast.

```css
--shadow-card:
  0 10px 28px rgba(16, 24, 40, 0.05);

--shadow-floating:
  0 12px 36px rgba(16, 24, 40, 0.08);
```

Do not use dark Material-style shadows.

## 8. Icon System

Use one icon library consistently, preferably Lucide.

Style:

```txt
inactive icon:
- black/dark gray
- outline
- stroke width ≈ 1.8–2
- no colored background

active bottom-nav icon:
- filled visual state
- dark/teal emphasis
```

If the icon library has no filled variant, use a paired filled icon asset/component rather than increasing stroke width.

Icons visible on Home:

- Bell
- Search
- CalendarCheck
- Inbox / ClipboardList
- Images / Image
- Album / BookHeart
- Users / Family
- Cake
- Child
- Baby
- UserCircle
- ShoppingBag
- ClipboardList
- Home

## 9. Buttons

### Primary CTA

```txt
height: 56–64px
padding-inline: 24–30px
background: teal
text: white
radius: pill
font-weight: 600
```

CTA in hero includes a left-pointing chevron due to RTL visual composition.

### Header icon buttons

```txt
size: 72–88px visual container on reference scale
background: rgba(255,255,255,.9)
border: subtle
radius: 22–26px
shadow: soft
```

For responsive implementation, scale these down proportionally on smaller devices.

## 10. Cards

All cards share:

```txt
background: white or warm-white
border: 1px subtle neutral
shadow: very soft
```

Avoid visible heavy outlines.

### Quick Action Card

- Vertical card.
- Large centered outline icon.
- Label centered.
- Spacious internal padding.
- Equal width for all four items.

### Category Card

- More compact than quick actions.
- Outline icon above.
- One-line category label.

## 11. Photography

Use real photographic assets with warm neutral lighting.

Required homepage assets:

```txt
logo.png
hero-baby.webp
portfolio-01.webp
portfolio-02.webp
portfolio-03.webp
order-thumbnail.webp
```

Image behavior:

```css
object-fit: cover;
object-position: center;
```

Hero should preserve the baby's face and composition on the right side.

Never place UI text permanently into the image assets.

## 12. Bottom Navigation

The bottom navigation is a floating iOS-inspired liquid-glass bar.

```css
background: rgba(255,255,255,.72);
backdrop-filter: blur(24px) saturate(140%);
-webkit-backdrop-filter: blur(24px) saturate(140%);
border: 1px solid rgba(255,255,255,.78);
```

Five items:

1. خانه
2. رزرو
3. سفارش‌ها
4. فروشگاه
5. پروفایل

Active state:

- Active icon is filled.
- Active label uses teal/dark teal.
- Active item may have a very subtle translucent teal highlight.
- Inactive icons remain black outline.
- Do not put every nav item in a separate pill.

## 13. Accessibility

- Minimum touch target: `44x44px`.
- Provide alt text for meaningful images.
- Icon-only buttons require `aria-label`.
- Maintain readable contrast.
- Do not rely on color alone to indicate active navigation; use filled icon state as well.

## 14. Tailwind Conventions

Map design tokens into Tailwind rather than scattering raw values.

Example:

```ts
// tailwind.config.ts
theme: {
  extend: {
    colors: {
      afraz: {
        DEFAULT: "#0A8098",
        dark: "#075E72",
        soft: "#DDF4F7"
      }
    }
  }
}
```

Prefer reusable utility compositions/components over repeated arbitrary values.

## 15. Source-of-Truth Rule

When implementing from the screenshot:

1. Follow this design system.
2. Follow the screenshot for visual proportions.
3. If they conflict, preserve the screenshot appearance.
4. Do not redesign or creatively reinterpret the approved Home screen.

## 16. Frontend Token Implementation

The runtime design system is split by responsibility:

```txt
src/frontend/src/styles/tokens.css      color, spacing, type, radius, shadow, icon, motion and layer values
src/frontend/src/styles/theme.css       Tailwind v4 theme mappings
src/frontend/src/styles/typography.css  semantic Persian typography roles
src/frontend/src/styles/utilities.css   layout, surface, icon, safe-area, glass and pattern utilities
src/frontend/src/assets/patterns/       reusable decorative SVG assets
```

Feature screens must use the semantic typography classes (`text-display`, `text-page-title`,
`text-section-title`, `text-card-title`, `text-body`, `text-label`, `text-caption`, and
`text-navigation`) or their matching tokens. Standard section rhythm uses `app-section`, with
`app-section--compact` and `app-section--spacious` only when the visual hierarchy requires it.

Icons use `app-icon` and the `xs`, `sm`, `md`, `lg`, and `xl` token scale. Decorative patterns
use `afraz-pattern` together with `afraz-pattern--sparkles` or `afraz-pattern--confetti`; pattern
layers are non-interactive and remain behind readable content.
