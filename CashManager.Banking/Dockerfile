FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./CashManager.Banking.Presentation/CashManager.Banking.Presentation.csproj" --disable-parallel
RUN dotnet publish "./CashManager.Banking.Presentation/CashManager.Banking.Presentation.csproj" -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal
WORKDIR /app
COPY --from=build /app ./

ENV ConnectionStrings__DefaultConnection="Server=127.0.0.1,1433;Database=CashManager.Banking;User Id=SA;Password=Password123!;TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "CashManager.Banking.Presentation.dll"]