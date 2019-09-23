FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["ClassApplication.csproj", "."]
RUN dotnet restore "ClassApplication.csproj"
COPY . .
RUN dotnet build "ClassApplication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ClassApplication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ClassApplication.dll"]