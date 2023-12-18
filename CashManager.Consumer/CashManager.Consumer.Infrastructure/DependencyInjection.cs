using CashManager.Consumer.Application.Articles;
using CashManager.Consumer.Application.CurrentUser;
using CashManager.Consumer.Application.HttpClients;
using CashManager.Consumer.Application.Security;
using CashManager.Consumer.Application.Token;
using CashManager.Consumer.Application.Transactions;
using CashManager.Consumer.Application.User;
using CashManager.Consumer.Infrastructure.Articles;
using CashManager.Consumer.Infrastructure.Context;
using CashManager.Consumer.Infrastructure.CurrentUser;
using CashManager.Consumer.Infrastructure.HttpClients;
using CashManager.Consumer.Infrastructure.Security;
using CashManager.Consumer.Infrastructure.Token;
using CashManager.Consumer.Infrastructure.Transactions;
using CashManager.Consumer.Infrastructure.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Consumer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CashManagerConsumerContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IHttpClientService, HttpClientService>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }
}
