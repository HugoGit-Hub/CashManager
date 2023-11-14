namespace CashManager.Banking.Domain.HttpClients;

public interface IHttpClientService
{
    public Task<HttpResponseMessage> Post<TDto>(string url, TDto dto, CancellationToken cancellationToken);
}