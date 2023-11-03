using Microsoft.Extensions.DependencyInjection;

namespace CashManager.Banking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}