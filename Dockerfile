# Preset Image for Dotnet REST Api for build
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
# Set Working Directory
WORKDIR /app
# Copy Project Files
COPY . .
# Restore Dependencies
RUN dotnet restore
# Build REST Api
RUN dotnet publish -o /app/published-app

# Preset Image for Dotnet REST Api for runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as runtime
# Set Working Directory
WORKDIR /app
# Copy Build Files
COPY --from=build /app/published-app /app
# Set Entrypoint
ENTRYPOINT [ "dotnet", "/app/Spg.TennisBooking.Api.dll" ]