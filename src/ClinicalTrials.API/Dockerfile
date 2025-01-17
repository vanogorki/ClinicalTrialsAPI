FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ClinicalTrials.API/ClinicalTrials.API.csproj", "src/ClinicalTrials.API/"]
COPY ["src/ClinicalTrials.Application/ClinicalTrials.Application.csproj", "src/ClinicalTrials.Application/"]
COPY ["src/ClinicalTrials.Domain/ClinicalTrials.Domain.csproj", "src/ClinicalTrials.Domain/"]
COPY ["src/ClinicalTrials.Infrastructure/ClinicalTrials.Infrastructure.csproj", "src/ClinicalTrials.Infrastructure/"]
COPY ["/Directory.Packages.props","/"]
COPY ["/Directory.Build.props","/"]

RUN dotnet restore "src/ClinicalTrials.API/ClinicalTrials.API.csproj"
COPY . .
WORKDIR "/src/src/ClinicalTrials.API"
RUN dotnet build "ClinicalTrials.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ClinicalTrials.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClinicalTrials.API.dll"]
