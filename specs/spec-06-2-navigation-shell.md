# Navbar Redesign Prompt — iOS 26 Liquid Glass

## Objective

Redesign the EXISTING bottom navigation bar of the app so it visually matches the supplied **iOS 26 Liquid Glass** reference as closely as possible.

The current navbar is too flat and too opaque. It looks like a rounded white card.

I want the new navbar to feel like a **floating optical glass control**, similar to the second reference image.

Do NOT redesign the navigation structure or change the menu items.

Keep:

```txt
خانه
رزرو
سفارش‌ها
فروشگاه
پروفایل
```

Keep the existing routing and RTL behavior.

Only redesign the visual treatment, active state, glass layers, spacing, animation, and interaction.

---

## Reference Repository

Use this repository as an implementation/reference source:

```txt
https://github.com/ryanashcraft/FabBar
```

Important:

FabBar is a SwiftUI/UIKit implementation, so do NOT blindly copy platform-specific code into the web frontend.

Instead, inspect it for these ideas:

- iOS 26 tab bar proportions
- floating capsule geometry
- segmented-control-like active selection behavior
- bubbly / lens-like active-state interaction
- safe-area positioning
- hardcoded sizing tuned to iOS 26
- spacing and padding choices
- smooth movement of the active selection

FabBar specifically uses UIKit internally and a segmented-control foundation to reproduce the interactive liquid-glass behavior.

For our frontend, recreate the same **visual principles** using Vue + CSS/Tailwind/SVG/filter techniques.

---

## 1. NON-NEGOTIABLE VISUAL RULE

Do NOT create generic glassmorphism.

This is NOT enough:

```css
background: rgba(255,255,255,.7);
backdrop-filter: blur(20px);
```

That looks like normal frosted glass.

I want a stronger iOS 26-inspired effect with:

- translucent optical glass
- visible background through the bar
- layered blur
- glass rim
- bright top highlight
- internal depth
- soft refraction-like feeling
- separate active glass lens
- smooth lens movement
- premium native-iOS appearance

If the result still looks like a white rounded rectangle, keep iterating.

---

## 2. Current Navbar Problems

The existing navbar currently looks approximately like:

```txt
opaque white capsule
flat background
weak transparency
minimal depth
no real optical rim
no refraction feeling
no active glass lens
```

These are the main problems to fix.

Do NOT simply reduce opacity by 5–10%.

Rebuild the optical layers.

---

## 3. Target Geometry

Primary target:

**iPhone 16 Pro Max**

Mobile-first implementation.

Recommended starting geometry:

```css
position: fixed;
left: 20px;
right: 20px;
bottom: calc(14px + env(safe-area-inset-bottom));
height: 74px;
z-index: 100;
```

Tune within:

```txt
horizontal inset: 20–28px
height: 72–82px
bottom offset: 12–22px + safe area
border radius: 34–42px
```

Use a continuous floating capsule.

Do NOT use a rectangular bottom bar.

---

## 4. Main Glass Surface

The navbar background must remain translucent.

Starting point:

```css
.liquid-nav {
  background:
    linear-gradient(
      180deg,
      rgba(255,255,255,.42) 0%,
      rgba(255,255,255,.20) 48%,
      rgba(255,255,255,.26) 100%
    );

  backdrop-filter:
    blur(22px)
    saturate(165%)
    contrast(104%);

  -webkit-backdrop-filter:
    blur(22px)
    saturate(165%)
    contrast(104%);
}
```

The page content behind the navbar must remain recognizable.

Do NOT use an opaque white layer behind it.

---

## 5. Glass Rim

The main capsule needs a clear but subtle optical rim.

Use:

```css
border: 1px solid rgba(255,255,255,.72);
```

Add an inner highlight:

```css
.liquid-nav::before {
  content: "";
  position: absolute;
  inset: 1px;
  border-radius: inherit;
  pointer-events: none;

  border: 1px solid rgba(255,255,255,.24);

  box-shadow:
    inset 0 1px 0 rgba(255,255,255,.82),
    inset 0 -1px 0 rgba(255,255,255,.12);
}
```

Top edge should catch more light than the bottom edge.

---

## 6. Specular Highlight

Add a soft upper reflection:

```css
.liquid-nav::after {
  content: "";
  position: absolute;
  inset: 0;
  border-radius: inherit;
  pointer-events: none;

  background:
    linear-gradient(
      180deg,
      rgba(255,255,255,.34) 0%,
      rgba(255,255,255,.10) 34%,
      rgba(255,255,255,0) 62%
    );

  mix-blend-mode: screen;
}
```

The reflection must be subtle.

Do NOT create a visible white stripe.

---

## 7. Outer Shadow

Use a very soft floating shadow:

```css
box-shadow:
  0 12px 30px rgba(15,23,42,.10),
  0 2px 8px rgba(15,23,42,.04),
  inset 0 1px 0 rgba(255,255,255,.70);
```

Avoid Material-style dark shadows.

---

## 8. Active Item = Separate Glass Lens

This is one of the most important requirements.

The selected navigation item must have a separate inner glass lens / bubble.

Do NOT represent selection only with a filled icon.

The active item should visually feel like a convex glass segment floating inside the main capsule.

Recommended:

```css
.nav-active-lens {
  position: absolute;
  top: 6px;
  bottom: 6px;
  width: calc(20% - 6px);

  border-radius: 30px;

  background:
    linear-gradient(
      180deg,
      rgba(255,255,255,.58),
      rgba(255,255,255,.21)
    );

  backdrop-filter:
    blur(12px)
    saturate(175%);

  -webkit-backdrop-filter:
    blur(12px)
    saturate(175%);

  border:
    1px solid rgba(255,255,255,.64);

  box-shadow:
    inset 0 1px 0 rgba(255,255,255,.90),
    inset 0 -1px 0 rgba(255,255,255,.16),
    0 5px 14px rgba(15,23,42,.06);
}
```

---

## 9. Active Lens Highlight

Add a localized highlight:

```css
.nav-active-lens::before {
  content: "";
  position: absolute;
  inset: 0;
  border-radius: inherit;
  pointer-events: none;

  background:
    radial-gradient(
      circle at 50% 0%,
      rgba(255,255,255,.65),
      rgba(255,255,255,.18) 42%,
      transparent 72%
    );
}
```

Keep it subtle.

---

## 10. One Movable Lens

Prefer ONE active lens that moves between tabs.

Do NOT render five different active backgrounds.

Recommended architecture:

```html
<nav class="liquid-nav">
  <div class="nav-active-lens" />

  <div class="nav-items">
    ...
  </div>
</nav>
```

Move the lens using transform based on selected tab.

This creates a more fluid iOS-like transition.

---

## 11. Active Lens Motion

Use smooth movement:

```css
transition:
  transform 320ms cubic-bezier(.2,.8,.2,1),
  width 280ms ease,
  background 220ms ease,
  box-shadow 220ms ease;
```

No large bounce.

No exaggerated spring effect.

A very subtle overshoot is acceptable only if it visually improves the iOS-like interaction.

---

## 12. Pressed / Bubbly Interaction

FabBar uses segmented-control behavior to reproduce the bubbly interactive glass effect.

Approximate this in the web implementation with a subtle press response.

On touch-down:

```css
.nav-item:active {
  transform: scale(.96);
}
```

The active lens may also briefly:

```txt
scale: 1.02
increase highlight slightly
increase inner glow slightly
```

Duration:

```txt
80–120ms
```

Keep the effect restrained.

---

## 13. Optional Refraction Approximation

CSS cannot exactly reproduce Apple's native optical shader.

Approximate the effect using:

- backdrop-filter
- layered transparency
- edge highlights
- inner shadows
- localized radial gradients
- active lens
- subtle color diffusion from the underlying page

Optional:

Use a lightweight SVG displacement/filter only if:

- it is performant
- it works in Safari
- it does not make the implementation brittle

Do NOT add WebGL or a large rendering library only for the navbar.

---

## 14. Background Must Be Visible

This is critical.

When content passes behind the navbar:

- colors should remain visible
- images should diffuse softly
- the navbar should visually inherit the background
- the bar must not become a flat white block

If everything behind the navbar becomes gray/white and indistinguishable:

reduce blur and/or opacity.

---

## 15. Icon Style

Keep the existing Instagram/iOS-like icon system.

Inactive:

```txt
outline
near-black
stroke ≈ 1.8–2px
```

Active:

```txt
filled icon
dark navy / near-black
```

Suggested:

```css
--nav-icon: #141B22;
--nav-active: #132633;
```

Do NOT use bright blue/purple active icons.

The selected state should be communicated primarily through:

1. active glass lens
2. filled icon
3. stronger label

---

## 16. Icon Size

Target:

```txt
25–29px
```

Use optical correction if some icons look visually heavier/larger.

All five items must appear balanced.

---

## 17. Labels

Keep:

```txt
خانه
رزرو
سفارش‌ها
فروشگاه
پروفایل
```

Approx:

```txt
font-size: 11–13px
line-height: 1.2
font-weight: 500
```

Active:

```txt
font-weight: 650–700
```

Color:

```css
#141A20
```

Inactive labels should NOT be very light gray.

---

## 18. Navigation Layout

Use five equal columns:

```css
.nav-items {
  position: relative;
  z-index: 2;

  display: grid;
  grid-template-columns: repeat(5, minmax(0, 1fr));
  height: 100%;
}
```

Each item:

```css
.nav-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 4px;
}
```

---

## 19. Active Home Dot

Keep the small red dot under `خانه` if it is part of the approved Home design.

Approx:

```txt
4–5px
```

Color:

```css
#FF3048
```

Do not enlarge it.

---

## 20. RTL

The app is Persian RTL.

Keep:

```html
dir="rtl"
```

Be careful when calculating the sliding active lens.

The visual item order must remain:

```txt
خانه
رزرو
سفارش‌ها
فروشگاه
پروفایل
```

from the RTL perspective already used by the app.

Do not accidentally reverse routing indexes while moving the lens.

---

## 21. Safe Area

Respect iPhone safe area:

```css
bottom:
  calc(14px + env(safe-area-inset-bottom));
```

Also add page bottom padding:

```css
padding-bottom:
  calc(110px + env(safe-area-inset-bottom));
```

The bar must not collide with the native Home indicator.

---

## 22. Mobile First

Base implementation must target mobile.

Primary device:

**iPhone 16 Pro Max**

Desktop/tablet adaptation is secondary.

Do not redesign the navigation into a sidebar unless explicitly requested elsewhere.

---

## 23. Vue Component Structure

Recommended:

```txt
components/navigation/
├── BottomNavigation.vue
├── BottomNavigationItem.vue
└── navigation-items.ts

styles/
└── liquid-glass.css
```

Keep navigation data separate from optical styling.

---

## 24. Suggested Vue Logic

Example concept:

```ts
const activeIndex = computed(() =>
  navItems.findIndex(item => item.routeName === route.name)
)
```

Then use active index to move the single lens.

Do NOT hardcode `خانه` as permanently active.

---

## 25. Lens Positioning

Use percentage/grid positioning rather than pixel offsets.

Concept:

```css
transform:
  translateX(calc(var(--active-index) * 100%));
```

Adjust for RTL correctly.

Prefer robust layout calculations rather than hardcoded x positions.

---

## 26. Performance

Must remain smooth in mobile Safari.

Avoid:

- many nested backdrop filters
- huge blur values
- full-screen blur
- JS animation loops
- expensive canvas rendering
- unnecessary WebGL

Prefer:

- CSS transforms
- opacity
- one main backdrop-filter
- one localized active-lens filter

---

## 27. Safari Support

Always include:

```css
backdrop-filter: ...;
-webkit-backdrop-filter: ...;
```

Test in Safari/WebKit if available.

---

## 28. Accessibility

Keep semantic navigation:

```html
<nav aria-label="ناوبری اصلی">
```

Each item must be a real link/button.

Minimum mobile touch target:

```txt
44 × 44px
```

Do not let decorative glass pseudo-elements intercept pointer events.

Use:

```css
pointer-events: none;
```

on optical layers.

---

## 29. Do Not Copy Unrelated FabBar Features

FabBar includes a floating action-button use case.

Our navbar currently has five normal navigation items.

Do NOT add a floating action button unless explicitly required.

Use FabBar only as a reference for:

- Liquid Glass look
- geometry
- active interaction
- safe area
- movement
- spacing

---

## 30. Visual Target

The final navbar should look much closer to the supplied iOS 26 reference than the current navbar.

Target characteristics:

```txt
floating capsule
high transparency
background visible through it
optical edge
soft top highlight
dimensional inner depth
glass active lens
smooth active movement
black iOS-like icons
compact labels
native-feeling proportions
```

---

## 31. Visual QA Is Mandatory

Do NOT stop after writing CSS.

Required loop:

```txt
inspect current navbar
      ↓
implement glass shell
      ↓
implement active lens
      ↓
run app
      ↓
capture iPhone 16 Pro Max screenshot
      ↓
compare with supplied iOS reference
      ↓
adjust opacity / blur / rim / geometry / lens
      ↓
capture again
      ↓
repeat
```

---

## 32. Comparison Priority

Fix differences in this order:

1. overall capsule shape
2. opacity/transparency
3. background visibility
4. active lens
5. lens size
6. edge highlight
7. blur
8. shadow
9. bottom/safe-area placement
10. icon size
11. label size
12. active animation
13. micro-spacing

---

## 33. Failure Conditions

### Wrong: White Rounded Card

If it still resembles the current navbar:

```txt
white rounded rectangle
```

it is wrong.

### Wrong: Generic Website Glassmorphism

If it looks like a typical SaaS blur card, it is not enough.

### Wrong: Excessive Blur

If the background is completely unrecognizable, reduce blur.

### Wrong: Excessive Transparency

If icons become difficult to read and the navigation visually disappears, increase local glass density while keeping the background visible.

### Wrong: Neon / Rainbow Effect

Do not use colorful glowing glass.

### Wrong: Static Active State

The active state should feel like a moving optical lens, not just a different icon color.

---

## 34. Acceptance Criteria

The task is complete only when:

- [ ] navbar is visibly translucent
- [ ] page colors/content remain visible through the glass
- [ ] outer optical rim exists
- [ ] subtle upper highlight exists
- [ ] active item has a separate glass lens
- [ ] lens moves smoothly between routes
- [ ] active icon is filled
- [ ] inactive icons are outline
- [ ] Persian labels remain correct
- [ ] RTL remains correct
- [ ] safe area is respected
- [ ] navbar floats above content
- [ ] no opaque white background remains
- [ ] no heavy Material shadow exists
- [ ] mobile Safari performance remains smooth
- [ ] iPhone 16 Pro Max visual QA has been performed
- [ ] result is clearly closer to iOS 26 Liquid Glass than the current navbar

---

## 35. Most Important Instruction

Do NOT merely make the existing navbar slightly more transparent.

Rebuild its visual treatment around the **iOS 26 Liquid Glass** concept.

Use the supplied iOS reference image as the visual source of truth.

Use the FabBar repository as a technical/reference inspiration for proportions and interaction behavior.

Keep the existing app navigation logic and Persian menu structure.

Continue iterating until the navbar no longer looks like a white card and instead feels like a real floating liquid-glass control.
