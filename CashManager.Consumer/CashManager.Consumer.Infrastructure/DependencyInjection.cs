using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Infrastructure.Articles;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IArticleRepository, ArticleRepository>();

        return services;
    }
}
