# ===========================================
# Build Stage
# ===========================================
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

# Copy project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . .

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# ===========================================
# Runtime Stage
# ===========================================
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

# ASP.NET Core listens on port 8080 inside the container
ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "Pre-perchase-server-app.dll"]