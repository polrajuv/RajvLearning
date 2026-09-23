# =========================
# Build stage
# =========================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file
COPY ["RajVLearningPortal_API.csproj", "./"]

# Restore NuGet packages
RUN dotnet restore "RajVLearningPortal_API.csproj"

# Copy the remaining source code
COPY . .

# Build and publish the application
RUN dotnet publish "RajVLearningPortal_API.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false


# =========================
# Runtime stage
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final

WORKDIR /app

# Application listens on port 8080 inside the container
EXPOSE 8080

# Copy published application
COPY --from=build /app/publish .

# Start the Web API
ENTRYPOINT ["dotnet", "RajVLearningPortal_API.dll"]