using CashManager.Banking.Domain.HttpClients;
using System.Text;
using System.Text.Json;

namespace CashManager.Banking.Infrastructure.HttpClients;

internal class HttpClientService : IHttpClientService
{
    public async Task Post<TDto>(string url, TDto dto, CancellationToken cancellationToken)
    {
        var uri = new Uri(url);
        var client = new HttpClient
        {
            BaseAddress = new Uri(uri.GetLeftPart(UriPartial.Authority))
        };

        var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(uri, content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpCallbackRequestException($"Something went wrong during the http callback request : {response.ReasonPhrase}");
        }
    }
}