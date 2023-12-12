FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CashManager.Banking/CashManager.Banking.Api/CashManager.Banking.Api.csproj", "CashManager.Banking/CashManager.Banking.Api/"]
RUN dotnet restore "CashManager.Banking/CashManager.Banking.Api/CashManager.Banking.Api.csproj"
COPY . .
WORKDIR "/src/CashManager.Banking/CashManager.Banking.Api"
RUN dotnet build "CashManager.Banking.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashManager.Banking.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashManager.Banking.Api.dll"]