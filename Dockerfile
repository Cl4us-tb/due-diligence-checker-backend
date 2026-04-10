# --- ETAPA DE BUILD ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["DueDiligenceChecker/DueDiligenceChecker.csproj", "DueDiligenceChecker/"]
RUN dotnet restore "DueDiligenceChecker/DueDiligenceChecker.csproj"

COPY . .
RUN dotnet publish "DueDiligenceChecker/DueDiligenceChecker.csproj" -c Release -o /app/publish --no-restore

# --- ETAPA DE RUNTIME ---
# Importante: el proyecto es net10.0, así que el runtime debe ser .NET 10 (no el de Playwright viejo).
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8080
ENV PLAYWRIGHT_BROWSERS_PATH=/ms-playwright
ENV DISPLAY=:99

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
        xvfb ca-certificates curl \
    && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .
RUN /app/.playwright/node/linux-x64/node /app/.playwright/package/cli.js install --with-deps chromium
COPY DueDiligenceChecker/docker/entrypoint.sh /app/entrypoint.sh

RUN chmod +x /app/entrypoint.sh

EXPOSE 8080
ENTRYPOINT ["/app/entrypoint.sh"]
