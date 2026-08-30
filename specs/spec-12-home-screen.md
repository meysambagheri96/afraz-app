# SPEC-12 — Home Screen

## Objective

- Implement the primary Persian RTL home screen based on the approved design direction based on this design `docs\design\homepage\home.png`.

Components:

docs/
├── design/
│   └── homepage/home.png
│
├── DESIGN-SYSTEM.md and `docs\design\designsystem.png`
├── UI-GUIDELINES.md 
└── SCREEN-HOME.md

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

- [ ] Layout fits iPhone 17 Pro Max scale.
- [ ] Section spacing and font scale are consistent.
- [ ] Home remains minimal above the fold.
- [ ] All actions navigate correctly.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
3. Produce a concise implementation plan listing affected modules/files.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
