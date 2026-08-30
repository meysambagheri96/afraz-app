# SPEC-02 — Vue Build Integration with ASP.NET Core

**Status:** Done

## Objective

- Serve the Vue production build from ASP.NET Core wwwroot.
- Keep Vite HMR for development.
- Ensure SPA history fallback works without swallowing /api routes.

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope

- Configure static file hosting.
- Configure SPA fallback to index.html.
- Add publish/build integration so Vue assets are present in wwwroot during Release publish.

## Frontend Scope

- Configure Vite output/copy strategy.
- Configure /api development proxy.
- Add environment-based API base configuration.

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

- [x] `dotnet publish` produces one deployable artifact containing Vue assets.
- [x] Direct navigation to `/booking` and `/profile` serves Vue.
- [x] `/api/*` never falls back to index.html.

## Required States / Failure Cases

Where applicable, implement and verify:

- Loading
- Empty
- Validation error
- Unauthorized / forbidden
- Not found
- Conflict / concurrency failure
- Network/server failure
- Retry
- Success

For payment-related stories also cover processing, cancelled and failed states.

## Testing

- Add focused unit tests for business rules.
- Add integration tests when database, authorization, payment, storage or concurrency behavior is involved.
- Add frontend component/E2E coverage for critical user paths where applicable.
- Do not remove failing tests to make the story pass.

## Definition of Done

- [x] Implementation follows project documentation and architecture.
- [x] Relevant build/type-check/lint/tests pass.
- [x] API contract is documented through OpenAPI where applicable.
- [x] Database migrations are added and reviewed where applicable.
- [x] UI uses the shared design system rather than duplicated styles.
- [x] RTL and iPhone safe areas are verified for customer-facing screens.
- [x] Security/authorization requirements are verified.
- [x] No secrets or generated noise are committed.
- [x] Any significant architecture decision is recorded as an ADR.
- [x] Story-specific acceptance criteria are satisfied.

## Codex Execution Instruction

Before coding:

1. Read the project reference, constitution, architecture and `AGENTS.md`.
2. Inspect existing code related to this feature.
3. Produce a concise implementation plan listing affected modules/files, database changes, API changes, frontend changes, tests and risks.
4. Implement only this story and required prerequisites.
5. Run the relevant validation commands.
6. Report completed work, tests executed and any remaining assumptions.
