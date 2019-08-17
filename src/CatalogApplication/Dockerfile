FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["CatalogApplication.csproj", "."]
RUN dotnet restore "CatalogApplication.csproj"
COPY . .
RUN dotnet build "CatalogApplication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "CatalogApplication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "CatalogApplication.dll"]