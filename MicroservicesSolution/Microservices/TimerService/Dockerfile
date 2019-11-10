FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Microservices/TimerService/TimerService.csproj", "Microservices/TimerService/"]
RUN dotnet restore "Microservices/TimerService/TimerService.csproj"
COPY . .
WORKDIR "/src/Microservices/TimerService"
RUN dotnet build "TimerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimerService.dll"]