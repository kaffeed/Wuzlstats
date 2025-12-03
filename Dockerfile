# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Install Node.js for npm commands
RUN apt-get update && apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /src

# Copy solution and project files
COPY Wuzlstats.sln .
COPY src/Wuzlstats/Wuzlstats.csproj src/Wuzlstats/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY . .

# Build and publish
WORKDIR /src/src/Wuzlstats
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

# Install libfontconfig1 for SkiaSharp
RUN apt-get update && apt-get install -y libfontconfig1 && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 5000

# Set environment variables
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "Wuzlstats.dll"]
