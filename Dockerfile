# Use official .NET SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS builder

WORKDIR /app

# Copy project files and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the full source code and build the application
COPY . . 
RUN dotnet publish -c Release -o /publish
# RUN dotnet publish

# Use a lightweight .NET runtime for production
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS runtime

WORKDIR /app
COPY --from=builder ./publish .

# # Expose the ports based on your launchSettings.json
EXPOSE 8080

# # Set environment variables
# ENV ASPNETCORE_URLS="https://+:7199;http://+:5144"
# ENV ASPNETCORE_ENVIRONMENT="Development"

# # Entry point to start the API
CMD ["dotnet", "CustomerDataPlatform.dll"]
