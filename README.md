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
dotnet build AfrazStudio.sln --configuration Release
dotnet test
npm ci --prefix src/frontend
npm run build --prefix src/frontend
npm run test --prefix src/frontend
npm run test:e2e --prefix src/frontend
```

The frontend build cleans and writes `src/frontend/dist`. Generated assets are ignored by Git.

## Frontend build and publish

Build the frontend by itself:

```powershell
npm ci --prefix src/frontend
npm run build --prefix src/frontend
```

The standalone frontend artifact is written to `src/frontend/dist`. The GitHub Pages workflow
publishes this directory. To publish the frontend together with the API, use the production publish
command below; the API project copies the frontend artifact into the published `wwwroot` directory.

## Production publish

Build and publish the complete application from the repository root:

```powershell
dotnet publish src/backend/Afraz.Api/Afraz.Api.csproj `
  --configuration Release `
  --output artifacts/afraz-publish
```

The Release build target runs `npm ci` and `npm run build` before ASP.NET Core collects static assets. The publish directory is a single deployable application containing the Vue SPA under `wwwroot`.

Run the published application:

```powershell
dotnet artifacts/afraz-publish/Afraz.Api.dll `
  --urls https://localhost:7080
```

The application is then available at `https://localhost:7080/`.

At runtime:

- `/api/*` is reserved for API endpoints; missing endpoints return Problem Details and never the SPA.
- `/health` exposes the application health endpoint.
- `/assets/*` serves Vite assets.
- Frontend paths such as `/orders`, `/booking`, `/profile`, and `/store` fall back to `index.html`.

## Database migrations

The EF Core CLI major version should match the project (`10.x`). Install it once, or use `update`
instead of `install` when an older global version already exists:

```powershell
dotnet tool install --global dotnet-ef --version 10.*
```

Add a migration from the repository root, replacing `<MigrationName>` with a descriptive name:

```powershell
dotnet ef migrations add <MigrationName> `
  --project src/backend/Afraz.Infrastructure/Afraz.Infrastructure.csproj `
  --startup-project src/backend/Afraz.Api/Afraz.Api.csproj `
  --context AfrazDbContext `
  --output-dir Persistence/Migrations
```

Apply pending migrations to the configured database:

```powershell
dotnet ef database update `
  --project src/backend/Afraz.Infrastructure/Afraz.Infrastructure.csproj `
  --startup-project src/backend/Afraz.Api/Afraz.Api.csproj `
  --context AfrazDbContext
```

`Update-Database` is the equivalent command only inside Visual Studio's **Package Manager Console**.
It is not a PowerShell command. In Package Manager Console, use:

```powershell
Update-Database -Project Afraz.Infrastructure -StartupProject Afraz.Api -Context AfrazDbContext
```

The commands use `ConnectionStrings__DefaultConnection` (or the corresponding value from the
active ASP.NET Core configuration). Review generated migrations before applying them outside local
development.

## Capacitor

The Capacitor configuration points to the same generated `wwwroot` directory. Add a native platform only when it is needed, then run `npm run cap:sync --prefix src/frontend` after a frontend build.

## Next project documents

When added, `AGENTS.md`, `docs/afraz-studio-reference.md`, and `docs/afraz-studio-constitution.md` become mandatory references for subsequent feature work.
