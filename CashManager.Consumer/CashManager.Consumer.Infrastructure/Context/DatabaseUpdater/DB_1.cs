using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Infrastructure.Context.DatabaseUpdater;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Infrastructure.Context;

public class DB_1 : IDbUpdater
{
    public void Update(IServiceProvider serviceProvider)
    {
        using var context = new CashManagerConsumerContext(
            serviceProvider.GetRequiredService<DbContextOptions<CashManagerConsumerContext>>());

        List<Article> articles = new()
        {
            new Article
            {
                Name = "Orange",
                Price = 2.99,
                Description = "Oranges des montagnes",
                ImageUrl = "lib/images/orange.jpg"
            },
            new Article
            {
                Name = "Champignon",
                Price = 0.59,
                Description = "Chammpignon des forets de l'ouest",
                ImageUrl = "lib/images/champignon.png"
            },
            new Article
            {
                Name = "Viande de boeuf",
                Price = 25.99,
                Description = "La côte de bœuf doit être bien épaisse, entre 4 et 8 cm pour qu'elle exprime toute sa saveur. Comptez entre 200 et 250 grammes par adulte pour une viande avec l'os. La taille de notre côte de bœuf peut varier, comptez entre 900g et 1100g par côte, soit pour 4 adultes.",
                ImageUrl = "lib/images/boeuf.jpg"
            },
            new Article
            {
                Name = "Sauce soja",
                Price = 14.99,
                Description = "Accompagnement indispensable de nos plateaux de makis et sushis, la sauce soja a beaucoup plus à offrir en cuisine",
                ImageUrl = "lib/images/sauceSoja.jpg"
            },
        };

        var allArticlesExist = context.Articles.Any(x => articles.Select(obj => obj.Name).Contains(x.Name));
        if (allArticlesExist)
        {
            return;
        }

        context.Articles.AddRange(articles);
        context.SaveChanges();
    }
}