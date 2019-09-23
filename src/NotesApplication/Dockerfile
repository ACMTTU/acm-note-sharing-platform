FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY ["NotesApplication.csproj", "."]
RUN dotnet restore "NotesApplication.csproj"
COPY . .
RUN dotnet build "NotesApplication.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NotesApplication.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "NotesApplication.dll"]