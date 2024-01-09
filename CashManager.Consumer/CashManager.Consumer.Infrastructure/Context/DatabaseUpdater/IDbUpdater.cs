namespace CashManager.Consumer.Infrastructure.Context.DatabaseUpdater;

public interface IDbUpdater 
{
    public void Update(IServiceProvider serviceProvider);
}