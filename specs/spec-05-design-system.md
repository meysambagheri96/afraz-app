# SPEC-05 — Initial Design System & Theme

## Objective

- Create the initial Afraz Studio visual system for a cheerful premium child/newborn photography app.
- Centralize colors, typography, spacing, radius, shadows, icon scale, z-index and motion.

docs/
├── design/
│   └── homepage/home.png
│
├── DESIGN-SYSTEM.md and `docs\design\designsystem.png`
├── UI-GUIDELINES.md 
└── SCREEN-HOME.md

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

- Create CSS variables/tokens.
- Use approved Persian sans-serif font family.
- Implement cheerful palette with brand teal plus controlled pink/yellow/mint/lilac accents.
- Add reusable SVG background pattern system.
- Define responsive typography for iPhone 17 Pro Max.
- Define section spacing and icon sizing rules.

## Acceptance Criteria

- [x] Tokens are documented.
- [x] Typography is consistent across nav, services, categories and body text.
- [x] No component needs ad-hoc theme values for standard use.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
