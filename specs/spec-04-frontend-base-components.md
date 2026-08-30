# SPEC-04 — Frontend Base UI Components

## Objective

- Create reusable low-level UI primitives before feature screens.

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

- Implement AppButton, AppIconButton, AppInput, AppTextarea, AppSelect, AppCheckbox, AppRadio, AppSwitch, AppBadge, AppAvatar, AppDivider, AppSkeleton, AppModal, AppBottomSheet.
- All components must support RTL, disabled/loading/error states, mobile touch targets, and design tokens.

## Technical Requirements

- Use Vue 3, TypeScript, Vite and the established design system for frontend work.
- Do not introduce a Generic Repository over EF Core.
- Keep endpoints thin and business rules server-side.
- Use ProblemDetails for API errors.
- Preserve RTL, safe-area and mobile WebView constraints.
- Avoid unrelated refactors.

## Acceptance Criteria

- [ ] Components are reusable and typed.
- [ ] No hard-coded random colors/spacing.
- [ ] RTL works correctly.
- [ ] Storybook is optional; a local component showcase page is acceptable.

## Definition of Done

- [ ] Implementation follows project documentation and architecture.
- [ ] Relevant build/type-check/lint.
- [ ] Database migrations are added and reviewed where applicable.
- [ ] UI uses the shared design system rather than duplicated styles.
- [ ] RTL and iPhone safe areas are verified for customer-facing screens.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
3. Produce a concise implementation plan listing affected modules/files, database changes, API changes, frontend changes, tests and risks.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Report completed work, tests executed and any remaining assumptions.
