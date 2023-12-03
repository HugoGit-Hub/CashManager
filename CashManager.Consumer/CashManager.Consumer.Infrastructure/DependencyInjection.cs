using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.HttpClients;
using CashManager.Consumer.Domain.Transactions;
using CashManager.Consumer.Infrastructure.Articles;
using CashManager.Consumer.Infrastructure.HttpClients;
using CashManager.Consumer.Infrastructure.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IHttpClientService, HttpClientService>();

        return services;
    }
}
