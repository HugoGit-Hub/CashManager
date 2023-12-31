﻿using CashManager.Banking.Application.HttpClients;
using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.CurrentUser;
using CashManager.Banking.Domain.Encryption;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Domain.User;
using CashManager.Banking.Infrastructure.Accounts;
using CashManager.Banking.Infrastructure.Authentication;
using CashManager.Banking.Infrastructure.CurrentUser;
using CashManager.Banking.Infrastructure.Encryption;
using CashManager.Banking.Infrastructure.HttpClients;
using CashManager.Banking.Infrastructure.Token;
using CashManager.Banking.Infrastructure.Transactions;
using CashManager.Banking.Infrastructure.User;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Banking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEncryptionService, EncryptionService>();
        services.AddScoped<IHttpClientService, HttpClientService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}