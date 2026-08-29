# Afraz Studio

Foundation monorepo for Afraz Studio. It combines a .NET 10 modular-monolith API and a Vue 3 RTL application into one production artifact. Business features are intentionally not included yet.

## Architecture

Backend dependencies point inward: `Afraz.Api` is the composition root, `Afraz.Infrastructure` implements technical concerns, `Afraz.Application` owns use-case contracts and CQRS behavior, and `Afraz.Domain` is independent. Feature code belongs in vertical slices under `Afraz.Application/Features`.

The Vue application uses TypeScript, Vite, Tailwind CSS, Pinia, Vue Router, Axios, VeeValidate, Zod, Motion Vue, and a Capacitor-ready configuration. Its production build is written to `src/backend/Afraz.Api/wwwroot` and is served by ASP.NET Core with history-mode fallback.

```text
src/
  backend/
    Afraz.Api/              ASP.NET Core host and wwwroot
    Afraz.Application/      CQRS and vertical slices
    Afraz.Domain/           domain model
    Afraz.Infrastructure/   EF Core, SQL Server, Redis
  frontend/                 Vue application
tests/
  Afraz.UnitTests/
  Afraz.IntegrationTests/
  frontend/                 Vitest and Playwright tests
docs/adr/                   architecture decisions
```

## Prerequisites

- .NET SDK 10
- Node.js 24+ and npm
- Docker Desktop or another Docker Compose runtime

## Local development

Create the local Compose environment file, then start SQL Server and Redis:

```powershell
Copy-Item .env.example .env
docker compose up -d
```

Run the API and frontend in separate terminals:

```powershell
dotnet run --project src/backend/Afraz.Api
npm run dev --prefix src/frontend
```

Vite runs at `http://localhost:5173` and proxies `/api` and `/health` to the API at `http://localhost:5080`. Override `VITE_DEV_API_TARGET` in `src/frontend/.env.development` when needed. Production uses same-origin requests; no production host is hard-coded.

Configuration values can be overridden with standard ASP.NET Core environment variables, for example `ConnectionStrings__DefaultConnection` and `Redis__ConnectionString`. The checked-in database password is a local-only default matching `.env.example`; production secrets must come from the deployment environment.

## Build and test

```powershell
dotnet restore
dotnet build
dotnet test
npm ci --prefix src/frontend
npm run build --prefix src/frontend
npm run test --prefix src/frontend
npm run test:e2e --prefix src/frontend
```

The frontend build cleans and writes `src/backend/Afraz.Api/wwwroot`. Generated assets are ignored by Git.

## Production publish

```powershell
dotnet publish src/backend/Afraz.Api -c Release
```

The Release build target runs `npm ci` and `npm run build` before ASP.NET Core collects static assets. The publish directory is a single deployable application containing the Vue SPA under `wwwroot`.

At runtime:

- `/api/*` is reserved for API endpoints; missing endpoints return Problem Details and never the SPA.
- `/health` exposes the application health endpoint.
- `/assets/*` serves Vite assets.
- Frontend paths such as `/orders`, `/booking`, `/profile`, and `/store` fall back to `index.html`.

## Capacitor

The Capacitor configuration points to the same generated `wwwroot` directory. Add a native platform only when it is needed, then run `npm run cap:sync --prefix src/frontend` after a frontend build.

## Next project documents

When added, `AGENTS.md`, `docs/afraz-studio-reference.md`, and `docs/afraz-studio-constitution.md` become mandatory references for subsequent feature work.
