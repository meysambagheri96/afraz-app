# SPEC-01 — Initial Infrastructure & Monorepo Foundation

**Status:** Done

## Objective

- Create a single Git repository for frontend and backend.
- Initialize .NET 10 solution and Vue 3 + TypeScript frontend.
- Add Docker Compose for SQL Server and Redis.
- Prepare docs/adr, tests, CI-ready structure.

## Business Context

This story is part of the Afraz Studio application. It must remain consistent with:

- `docs/afraz-studio-reference.md`
- `docs/afraz-studio-constitution.md`
- `docs/afraz-studio-architecture.md`
- `AGENTS.md`

All customer-facing UI must be Persian, RTL, mobile-first, and optimized for the iPhone 17 Pro Max design target unless this story is backend-only.

## Backend Scope

- Create Afraz.Api, Afraz.Application, Afraz.Domain, Afraz.Infrastructure.
- Configure .NET 10, nullable, OpenAPI, ProblemDetails, Serilog, health checks.
- Add EF Core 10 + SQL Server connection skeleton.
- Add Redis registration skeleton.

## Frontend Scope

- Initialize Vue 3 + TypeScript + Vite.
- Add Tailwind CSS, Pinia, Vue Router, Axios, VeeValidate, Zod, Motion Vue.
- Prepare Capacitor-ready structure.
- Configure RTL and mobile viewport baseline.

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

- [x] dotnet build succeeds.
- [x] frontend production build succeeds.
- [x] docker compose starts SQL Server and Redis.
- [x] Repository matches documented monorepo structure.

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
