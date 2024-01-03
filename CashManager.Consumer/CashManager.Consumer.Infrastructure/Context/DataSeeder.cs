using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Infrastructure.Context;

public class DataSeeder
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new CashManagerConsumerContext(
            serviceProvider.GetRequiredService<DbContextOptions<CashManagerConsumerContext>>());

        if (context.Articles.Any() || context.Users.Any())
        {
            return;
        }

        List<Article> articles = new()
        {
            new Article
            {
                Name = "Patates",
                Price = 9.99,
                Description = "Patates de référence chez nous, 100% francaise",
                ImageUrl = "lib/images/top-view-raw-potatoes-table.jpg"
            },
            new Article
            {
                Name = "Tomate",
                Price = 5.99,
                Description = "Tomate de référence chez nous, 100% francaise",
                ImageUrl = "lib/images/tomate.jpeg"
            },
            new Article
            {
                Name = "Banane",
                Price = 0.99,
                Description = "Les bananes les moins chères du marché !!! pas française à 100%",
                ImageUrl = "lib/images/bananas-1119790_1280.jpg"
            },
            new Article
            {
                Name = "Carrotes",
                Price = 0.99,
                Description = "Les Carottes les moins chères du marché !!! pas françaises à 100%",
                ImageUrl = "lib/images/premium-fresh-organic-carrots.jpg"
            },
            new Article
            {
                Name = "Choux fleur",
                Price = 0.99,
                Description = "Les Choux les moins chères du marché !!! pas français à 100%",
                ImageUrl = "lib/images/cauliflower-isolated.jpg"
            }
        };

        context.Articles.AddRange(articles);
        context.SaveChanges();

        var user = new Users
        {
            Email = "user@example.com",
            Firstname = "string",
            Lastname = "string",
            Password = "PmyaNrHS1pfIS8AsY/9e+8a4m5zP39NdX+tzDU37zeFHCMEgF2VT91z5iHO/wChaNUDvyhDSForZqCXpTBpFuA==",
        };

        context.Users.Add(user);
        context.SaveChanges();
    }
}