# SPEC-12 — Home Screen

## Objective

- Implement the primary Persian RTL home screen based on the approved design direction based on this design `docs\design\homepage\home.png`.

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

## Technical Requirements

- Follow the existing Modular Monolith + Clean Architecture + Vertical Slice + CQRS conventions.
- Use .NET 10, ASP.NET Core, EF Core 10 and SQL Server for backend work.
- Use Vue 3, TypeScript, Vite and the established design system for frontend work.
- Do not introduce a Generic Repository over EF Core.
- Keep endpoints thin and business rules server-side.
- Use explicit request/response contracts.
- Use FluentValidation for backend validation where applicable.
- Use ProblemDetails for API errors.
- Propagate `CancellationToken` on backend I/O paths.
- Never trust client-provided prices, ownership, payment status or availability.
- Add authorization to all customer-owned resources.
- Add tests for business-critical behavior introduced by this story.
- Preserve RTL, safe-area and mobile WebView constraints.
- Avoid unrelated refactors.

## Acceptance Criteria

- [ ] Layout fits iPhone 17 Pro Max scale.
- [ ] Section spacing and font scale are consistent.
- [ ] Home remains minimal above the fold.
- [ ] All actions navigate correctly.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
3. Produce a concise implementation plan listing affected modules/files, database changes, API changes, frontend changes, tests and risks.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Report completed work, tests executed and any remaining assumptions.
