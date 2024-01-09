using CashManager.Consumer.Infrastructure.Context.DatabaseUpdater;

namespace CashManager.Consumer.Api.Configuration.Middlewares;

public class DatabaseUpdaterMiddleware
{
    public static void Update() 
    {
        var  types = AssemblyMarker.Assembly
            .GetTypes()
            .Where( x => x.GetInterfaces().Contains(typeof(IDbUpdater)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var method = type.GetMethod("Update");
            method?.Invoke(instance, null);
        }
    }
}