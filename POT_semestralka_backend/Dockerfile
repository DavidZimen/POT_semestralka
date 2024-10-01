FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["POT_semestralka_backend/POT_semestralka_backend.csproj", "POT_semestralka_backend/"]
RUN dotnet restore "POT_semestralka_backend/POT_semestralka_backend.csproj"
COPY . .
WORKDIR "/src/POT_semestralka_backend"
RUN dotnet build "POT_semestralka_backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "POT_semestralka_backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "POT_semestralka_backend.dll"]
