using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Domain.User;
using CashManager.Banking.Infrastructure.Authentication;
using CashManager.Banking.Infrastructure.Encryption;
using CashManager.Banking.Infrastructure.Token;
using CashManager.Banking.Infrastructure.Transactions;
using CashManager.Banking.Infrastructure.User;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Banking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddScoped<IEncryptionService, EncryptionService>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<ITransactionRepository, TransactionRepository>();

        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}