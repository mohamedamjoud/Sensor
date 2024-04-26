FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Adapter.Api/Adapter.Api.csproj", "Adapter.Api/"]
COPY ["Adapter.Api/Temperature.db", "Adapter.Api/"]
COPY ["Adapter.SQLLit/Adapter.SQLLit.csproj", "Adapter.SQLLit/"]
COPY ["Adapter.TemperatureSensor/Adapter.TemperatureSensor.csproj", "Adapter.TemperatureSensor/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore "Adapter.Api/Adapter.Api.csproj"
COPY . .
WORKDIR "/src/Adapter.Api"
RUN dotnet build "Adapter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Adapter.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /src/Adapter.Api/Temperature_Prod.db .
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Adapter.Api.dll"]
