FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["UserApplication.csproj", "."]
RUN dotnet restore "UserApplication.csproj"
COPY . .
RUN dotnet build "UserApplication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserApplication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserApplication.dll"]