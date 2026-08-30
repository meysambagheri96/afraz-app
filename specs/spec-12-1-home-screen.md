# SPEC-12 — Home Screen

## Objective

- Implement the primary Persian RTL home screen based on the approved design direction based on this design `docs\design\homepage\home.png`.
- Implement the Home screen based on:

### Requirements:

- Reproduce the reference as closely as possible.
- Persian RTL layout.
- Target viewport: iPhone 17 Pro Max.
- Respect iOS safe areas.
- Add Dynamic Island.
- Use reusable components.
- Do not use the screenshot itself as UI.
- Use actual layout, text, icons and image assets.
- Use monochrome Instagram-style icons.
- Inactive navigation icons: black outline.
- Active navigation icon: black filled.
- Bottom navigation must use an iOS liquid-glass appearance.
- Match spacing, typography, corner radius and proportions from the reference.
- Keep all sizing responsive rather than hardcoding the entire screen.
- Extract repeated dimensions/colors into design tokens.

### Before implementation:
1. Analyze the screenshot.
2. Identify all visual sections.
3. Define components and design tokens.
4. Implement the screen.
5. Run the app and take a screenshot.
6. Compare the implementation against the reference.
7. Iteratively fix visual differences until it closely matches the reference.

Do not redesign or creatively interpret the UI.
The uploaded reference is the source of truth.

###

Steps:
Reference Image
      ↓
Design Specification
      ↓
Design Tokens
      ↓
Reusable Components
      ↓
Codex Implementation
      ↓
Screenshot
      ↓
Visual Comparison
      ↓
Pixel-perfect fixes

### Guides:
docs/
├── design/
│   └── homepage/home.png
│
├── DESIGN-SYSTEM.md and `docs\design\designsystem.png`
├── UI-GUIDELINES.md 
└── SCREEN-HOME.md

## Components:

HomeScreen
├── StatusBar
├── DynamicIsland
├── Header
├── HeroBanner
├── QuickActions
├── FeaturedPortfolio
├── Categories
├── LastOrderCard
└── BottomNavigation

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/adr/0002-frontend.md`
- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope

- No backend implementation is required in this story unless needed to support the frontend contract.

## Frontend Scope

- Header with studio title on right, Dynamic Island safe area, search and notifications.
- Story-like category shortcuts.
- Full-width hero banner with booking CTA.
- Quick business actions.
- Horizontally scrollable Featured Portfolio.
- Photography categories after featured portfolio.
- Context card for photos/orders/bookings.
- Album store preview.
- Lower studio/contact/legal sections.
- Cheerful SVG background details.
- Use no-radius/full-width photography where required by final design.

## Acceptance Criteria

- [x] Layout fits iPhone 17 Pro Max scale.
- [x] Section spacing and font scale are consistent.
- [x] Home remains minimal above the fold.
- [x] All actions navigate correctly.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
