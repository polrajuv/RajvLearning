# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project file
COPY ["RajVLearning.API.csproj", "./"]

# Restore NuGet packages
RUN dotnet restore "RajVLearning.API.csproj"

# Copy the remaining source code
COPY . .

# Build and publish
RUN dotnet publish "RajVLearning.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-restore


# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Copy published application
COPY --from=build /app/publish .

# Application listens on HTTP port 8080 inside the container
ENV ASPNETCORE_HTTP_PORTS=8080

# Document the container port
EXPOSE 8080

# Start the API
ENTRYPOINT ["dotnet", "RajVLearning.API.dll"]