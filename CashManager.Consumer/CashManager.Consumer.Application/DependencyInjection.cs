using CashManager.Consumer.Application.Articles;
using CashManager.Consumer.Application.Transactions;
using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.Transactions;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ITransactionService, TransactionService>();
        
        return services;
    }
}
