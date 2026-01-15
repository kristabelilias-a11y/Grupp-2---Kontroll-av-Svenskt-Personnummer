# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy solution and project files
COPY Personnummer.sln .
COPY Personnummer-App/Personnummer-App.csproj ./Personnummer-App/
COPY Personnummer-Kontroll.Test/Personnummer-Kontroll.Test.csproj ./Personnummer-Kontroll.Test/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build and publish the application
WORKDIR /app/Personnummer-App
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/runtime:9.0 AS runtime
WORKDIR /app

# Copy the published application from build stage
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "Personnummer-App.dll"]