﻿using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Application.Accounts.GetAccountByCurrentUser;
using CashManager.Banking.Application.Transactions;
using CashManager.Banking.Application.User;
using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.ErrorHandling;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Domain.User;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Banking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IUserService, UserService>();

        services.AddTransient<IRequestHandler<GetAccountsByCurrentUserQuery, Result<IEnumerable<AccountResponse>>>, GetAccountsByCurrentUserQueryHandler>();

        return services;
    }
}