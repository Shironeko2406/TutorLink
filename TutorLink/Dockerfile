# Use the official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# Copy and restore the project
COPY *.csproj .
RUN dotnet restore

# Copy everything else and build the project
COPY . .
RUN dotnet publish -c Release -o /app

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app .

# Set the entry point for the application
ENTRYPOINT ["dotnet", "TutorLink.dll"]
