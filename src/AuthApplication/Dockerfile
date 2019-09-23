FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["AuthApplication.csproj", "."]
RUN dotnet restore "AuthApplication.csproj"
COPY . .
RUN dotnet build "AuthApplication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AuthApplication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AuthApplication.dll"]