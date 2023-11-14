using CashManager.Banking.Domain.HttpClients;
using System.Net.Http.Json;

namespace CashManager.Banking.Infrastructure.HttpClients;

internal class HttpClientService : IHttpClientService
{
    public async Task<HttpResponseMessage> Post<TDto>(string url, TDto dto, CancellationToken cancellationToken)
    {
        var uri = new Uri(url);
        var client = new HttpClient
        {
            BaseAddress = new Uri(uri.GetLeftPart(UriPartial.Authority))
        };

        return await client.PostAsJsonAsync(uri.LocalPath, dto, cancellationToken);
    }
}