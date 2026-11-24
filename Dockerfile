# ==============================================================================
# NEXORA — Production Dockerfile for ASP.NET Core & EF Core
# Suitable for deployment on Railway, Render, Fly.io, Azure App Service, AWS ECS
# ==============================================================================

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy project definition and restore dependencies
COPY ["Nexora.csproj", "./"]
RUN dotnet restore "Nexora.csproj"

# Copy source and publish release build
COPY . .
RUN dotnet publish "Nexora.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Nexora.dll"]
