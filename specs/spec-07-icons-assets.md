# SPEC-07 — Custom Iconography & Asset Pipeline

## Objective

- Create a consistent icon system inspired by Instagram/Material/SF-style simplicity while matching Afraz brand palette.
- Sample icons are here `docs\design\icons\services-icons.png` and `docs\design\icons\categories-icons.png` 
  
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

- Create custom SVG icons for main navigation and business actions.
- Provide a wrapper component for icon size/stroke/active color.
- Organize photography images, patterns, placeholders and logos.

## Acceptance Criteria

- [x] Icons use consistent stroke/scale.
- [x] No uncontrolled mix of icon libraries.
- [x] All icons are accessible and theme-aware.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
