namespace CashManager.Banking.Domain.HttpClients;

public interface IHttpClientService
{
    public HttpResponseMessage Post<TDto>(string url, TDto dto);
}