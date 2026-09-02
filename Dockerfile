# syntax=docker/dockerfile:1

FROM node:24-alpine AS frontend-build
WORKDIR /src/frontend

COPY src/frontend/package.json src/frontend/package-lock.json ./
RUN npm ci

COPY src/frontend/ ./
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS backend-build
WORKDIR /src

COPY Directory.Build.props AfrazStudio.sln ./
COPY src/backend/Afraz.Api/Afraz.Api.csproj src/backend/Afraz.Api/
COPY src/backend/Afraz.Application/Afraz.Application.csproj src/backend/Afraz.Application/
COPY src/backend/Afraz.Domain/Afraz.Domain.csproj src/backend/Afraz.Domain/
COPY src/backend/Afraz.Infrastructure/Afraz.Infrastructure.csproj src/backend/Afraz.Infrastructure/
RUN dotnet restore src/backend/Afraz.Api/Afraz.Api.csproj

COPY src/backend/ src/backend/
RUN dotnet publish src/backend/Afraz.Api/Afraz.Api.csproj \
    --configuration Release \
    --output /app/publish \
    --no-restore \
    /p:BuildFrontend=false

# Serve the generated SPA from the same origin root as the API host.
COPY --from=frontend-build /src/frontend/dist/ /app/publish/wwwroot/

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080 \
    ASPNETCORE_ENVIRONMENT=Production \
    TZ=Asia/Tehran \
    DOTNET_EnableDiagnostics=0

COPY --from=backend-build /app/publish/ ./

EXPOSE 8080
USER $APP_UID
ENTRYPOINT ["dotnet", "Afraz.Api.dll"]
