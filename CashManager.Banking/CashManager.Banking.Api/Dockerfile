FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CashManager.BankingApi/CashManager.BankingApi.csproj", "CashManager.BankingApi/"]
RUN dotnet restore "CashManager.BankingApi/CashManager.BankingApi.csproj"
COPY . .
WORKDIR "/src/CashManager.BankingApi"
RUN dotnet build "CashManager.BankingApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CashManager.BankingApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CashManager.BankingApi.dll"]