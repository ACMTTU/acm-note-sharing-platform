FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["ClaimsService.csproj", "."]
RUN dotnet restore "ClaimsService.csproj"
COPY . .
RUN dotnet build "ClaimsService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ClaimsService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ClaimsService.dll"]