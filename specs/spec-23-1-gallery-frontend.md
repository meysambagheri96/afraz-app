# SPEC-23 — Private Gallery Frontend

## Objective

- Implement high-performance customer gallery UX.

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

- Grid, fullscreen viewer, swipe, zoom, favorite, multi-select, selected count, filters.
- Use thumbnail-first loading and high-res on demand.
- Virtualize if gallery size requires it.


## Acceptance Criteria

- [ ] Smooth on large galleries.
- [ ] Selection state is robust across pagination.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Mark tasks current file as done with [x]
