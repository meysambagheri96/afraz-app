# SPEC-06 — Application Shell & Liquid Glass Navigation

## Objective

- Create the global mobile application shell.
- Implement iOS-inspired floating Liquid Glass bottom navigation.

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

- Tabs: خانه, رزرو, سفارش‌ها, فروشگاه, پروفایل.
- Respect Dynamic Island/top safe area and bottom home indicator.
- Implement active state, blur/translucency, correct icon scale, RTL ordering.
- Ensure content is not hidden behind nav.

## Acceptance Criteria

- [ ] Works on iPhone 17 Pro Max viewport.
- [ ] Bottom nav remains readable on varying backgrounds.
- [ ] Navigation works with Vue Router.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
