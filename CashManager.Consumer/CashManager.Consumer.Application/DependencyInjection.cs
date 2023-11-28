using CashManager.Consumer.Application.Articles;
using CashManager.Consumer.Domain.Articles;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        
        return services;
    }
}
