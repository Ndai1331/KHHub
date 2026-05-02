# syntax=docker/dockerfile:1
# Build from KHHub repository root:
#   docker build -f apps/web/KHHub.Web/Dockerfile --platform linux/amd64 --build-arg RUNTIME_ID=linux-x64 -t longnguyen1331/web:latest .

ARG BUILD_CONFIGURATION=Release
ARG RUNTIME_ID=linux-x64

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION
ARG RUNTIME_ID

RUN apt-get update \
    && apt-get install -y --no-install-recommends curl ca-certificates \
    && curl -fsSL https://deb.nodesource.com/setup_22.x | bash - \
    && apt-get install -y --no-install-recommends nodejs \
    && corepack enable \
    && apt-get clean && rm -rf /var/lib/apt/lists/*

RUN dotnet tool install -g Volo.Abp.Cli --version 10.3.0
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /src
COPY . .

WORKDIR /src/apps/web/KHHub.Web
RUN abp install-libs
RUN dotnet restore
RUN dotnet publish "KHHub.Web.csproj" -c "${BUILD_CONFIGURATION}" -r "${RUNTIME_ID}" --self-contained false -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "KHHub.Web.dll"]
