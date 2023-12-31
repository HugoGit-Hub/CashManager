﻿using CashManager.Consumer.Application.Articles;
using CashManager.Consumer.Application.Articles.GetArticleById;
using CashManager.Consumer.Application.Articles.GetArticles;
using CashManager.Consumer.Application.Authentication.Login;
using CashManager.Consumer.Application.Authentication.Register;
using CashManager.Consumer.Application.CartItems;
using CashManager.Consumer.Application.CartItems.CreateCartItem;
using CashManager.Consumer.Application.ShoppingSessions;
using CashManager.Consumer.Application.ShoppingSessions.DeleteCartItemFromCurrentShoppingSession;
using CashManager.Consumer.Application.ShoppingSessions.GetCurrentOpenedShoppingSessionCartItems;
using CashManager.Consumer.Application.ShoppingSessions.GetCurrentShoppingSession;
using CashManager.Consumer.Application.ShoppingSessions.UpdateCartItemFromCurrentShoppingSession;
using CashManager.Consumer.Application.Transactions;
using CashManager.Consumer.Application.Transactions.CreateTransaction;
using CashManager.Consumer.Application.Transactions.ValidateTransaction;
using CashManager.Consumer.Application.User;
using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.ShoppingSessions;
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
        services.AddScoped<ICartItemService, CartItemService>();
        services.AddScoped<IShoppingSessionService, ShoppingSessionService>();

        services.AddScoped<IRequestHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();
        services.AddScoped<IRequestHandler<LoginQuery, Result<string>>, LoginQueryHandler>();
        services.AddScoped<IRequestHandler<GetArticleByIdQuery, Result<ArticleResponse>>, GetArticleByIdQueryHandler>();
        services.AddScoped<IRequestHandler<GetArticlesQuery, Result<IEnumerable<ArticleResponse>>>, GetArticlesQueryHandler>();
        services.AddScoped<IRequestHandler<CreateTransactionCommand, Result>, CreateTransactionCommandHandler>();
        services.AddScoped<IRequestHandler<ValidateTransactionCommand, Result>, ValidateTransactionCommandHandler>();
        services.AddScoped<IRequestHandler<GetShoppingSessionCartItemsQuery, Result<IEnumerable<GetShoppingSessionCartItemsResponse>>>, GetShoppingSessionCartItemsQueryHandler>();
        services.AddScoped<IRequestHandler<CreateCartItemCommand, Result<CreateCartItemResponse>>, CreateCartItemCommandHandler>();
        services.AddScoped<IRequestHandler<GetCurrentShoppingSessionQuery, Result<GetCurrentShoppingSessionResponse>>, GetCurrentShoppingSessionQueryHandler>();
        services.AddScoped<IRequestHandler<DeleteCartItemFromCurrentShoppingSessionCommand, Result>, DeleteCartItemFromCurrentShoppingSessionCommandHandler>();
        services.AddScoped<IRequestHandler<UpdateCartItemFromCurrentShoppingSessionCommand, Result>, UpdateCartItemFromCurrentShoppingSessionCommandHandler>();

        return services;
    }
}
