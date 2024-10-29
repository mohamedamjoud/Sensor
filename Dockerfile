FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY ["Sensor.Api/Sensor.Api.csproj", "Sensor.Api/"]
COPY ["Sensor.Api/DataBase/Temperature.db", "Sensor.Api/"]
COPY ["Adapter.SQLite/Adapter.SQLite.csproj", "Adapter.SQLite/"]
COPY ["Adapter.TemperatureSensor/Adapter.TemperatureSensor.csproj", "Adapter.TemperatureSensor/"]
COPY ["Core/Core.csproj", "Core/"]

RUN dotnet restore "Sensor.Api/Sensor.Api.csproj"
COPY . .
WORKDIR "/src/Sensor.Api"
RUN dotnet build "Sensor.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Sensor.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /src/Sensor.Api/DataBase/Temperature_Prod.db .
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sensor.Api.dll"]
