FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CashManager.Consumer/CashManager.Consumer.Api/CashManager.Consumer.Api.csproj", "CashManager.Consumer/CashManager.Consumer.Api/"]
RUN dotnet restore "CashManager.Consumer/CashManager.Consumer.Api/CashManager.Consumer.Api.csproj"
COPY . .
WORKDIR "/src/CashManager.Consumer/CashManager.Consumer.Api"
RUN dotnet build "CashManager.Consumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CashManager.Consumer.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashManager.Consumer.Api.dll"]