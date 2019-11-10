FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Tests/TestWebApplication/TestWebApplication.csproj", "Tests/TestWebApplication/"]
RUN dotnet restore "Tests/TestWebApplication/TestWebApplication.csproj"
COPY . .
WORKDIR "/src/Tests/TestWebApplication"
RUN dotnet build "TestWebApplication.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestWebApplication.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestWebApplication.dll"]