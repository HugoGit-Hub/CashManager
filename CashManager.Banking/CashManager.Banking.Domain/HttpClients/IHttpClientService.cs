namespace CashManager.Banking.Domain.HttpClients;

public interface IHttpClientService
{
    public Task Post<TDto>(string url, TDto dto, CancellationToken cancellationToken);
}