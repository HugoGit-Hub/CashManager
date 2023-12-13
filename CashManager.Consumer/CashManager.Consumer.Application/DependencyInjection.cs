using CashManager.Consumer.Application.Articles;
using CashManager.Consumer.Application.Authentication.Register;
using CashManager.Consumer.Application.Transactions;
using CashManager.Consumer.Application.User;
using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.Transactions;
using CashManager.Consumer.Domain.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IRequestHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();
        
        return services;
    }
}
