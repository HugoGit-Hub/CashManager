using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Banking.Infrastructure.Context;

public static class DataSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new CashManagerBankingContext(
            serviceProvider.GetRequiredService<DbContextOptions<CashManagerBankingContext>>());

        if (!context.Users.Any() && context.Accounts.Any())
        {
            return;
        }
        
        var consumerUser = new Users
        {
            Bank = BankAccountEnum.Cic,
            CreatedAt = DateTime.Now.Date,
            Email = "user@example.com",
            Firstname = "string",
            Lastname = "string",
            Password = "PmyaNrHS1pfIS8AsY/9e+8a4m5zP39NdX+tzDU37zeFHCMEgF2VT91z5iHO/wChaNUDvyhDSForZqCXpTBpFuA==",
        };

        var consumerAccount = new Account
        {
            Owner = "Consumer application",
            Number = "1234567890123456789012345678901234",
            Value = 10000.90,
            CloseDateTime = DateTime.Now.Date,
            OpenDateTime = DateTime.Now.Date,
            User = consumerUser,
            UserId = consumerUser.Id
        };

        context.Users.Add(consumerUser);
        context.Accounts.Add(consumerAccount);

        var clientUser = new Users
        {
            Bank = BankAccountEnum.BnpParibas,
            CreatedAt = DateTime.Now.Date,
            Email = "client@gmail.com",
            Firstname = "client",
            Lastname = "client",
            Password = "J2Vi1mxV43A041X9DqCQiRDMisHrErsLlGzzDpR3t3hPaPWEjARZwN4sWOwWmTS/HPTJ9PRWnrEgCIA+uu295g==",
        };

        var clientAccount = new Account
        {
            Owner = "Client application",
            Number = "string",
            Value = 600.89,
            CloseDateTime = DateTime.Now.Date,
            OpenDateTime = DateTime.Now.Date,
            User = clientUser,
            UserId = clientUser.Id
        };

        context.Users.Add(clientUser);
        context.Accounts.Add(clientAccount);

        context.SaveChanges();
    }
}