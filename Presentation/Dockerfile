FROM nginx:alpine AS base
WORKDIR /app
EXPOSE 8080

# Stage 1: Build the Blazor WebAssembly application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY Presentation/Presentation.csproj Presentation/
RUN dotnet restore Presentation/Presentation.csproj

# Copy the rest of the application source code and build it
COPY . .
WORKDIR /src/Presentation
RUN dotnet build Presentation.csproj -c Release -o /app/build

# Publish the Application
FROM build AS publish
RUN dotnet publish Presentation.csproj -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Serve the Blazor app with Nginx
FROM base AS final
WORKDIR /app

# Copy only the static files from the build stage
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html

# Copy the Nginx server configuration
COPY Presentation/nginx.conf /etc/nginx/nginx.conf
