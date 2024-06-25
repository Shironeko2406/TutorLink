FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Development

# Copy the project file and restore dependencies
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY TutorLinkAPI/TutorLinkAPI.csproj TutorLinkAPI/
# Copy the application code
COPY . .
RUN dotnet restore TutorLinkAPI/TutorLinkAPI.csproj

# Build the application
WORKDIR /src/TutorLinkAPI
RUN dotnet build -c Release -o /app

# Publish the application
FROM build AS publish
RUN dotnet publish -c Release -o /app

# Use the base image and copy the published files
FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# Run the application when the container starts

ENTRYPOINT ["dotnet", "TutorLinkAPI.dll"]