# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT=Development

# Image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore dependencies (caching layers)
COPY ["LibraryProject.WebAPI/LibraryManagementSystem.csproj", "LibraryProject.WebAPI/"]
COPY ["LibraryProject.Application/LibraryProject.Application.csproj", "LibraryProject.Application/"]
COPY ["LibraryProject.Domain/LibraryProject.Domain.csproj", "LibraryProject.Domain/"]
COPY ["LibraryProject.Infrastructure/LibraryProject.Infrastructure.csproj", "LibraryProject.Infrastructure/"]

RUN dotnet restore "LibraryProject.WebAPI/LibraryManagementSystem.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/LibraryProject.WebAPI"
RUN dotnet build "LibraryManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LibraryManagementSystem.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: copy published files and run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagementSystem.dll"]
