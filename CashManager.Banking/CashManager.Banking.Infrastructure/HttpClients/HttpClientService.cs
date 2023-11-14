using CashManager.Banking.Domain.HttpClients;
using System.Text;
using System.Text.Json;

namespace CashManager.Banking.Infrastructure.HttpClients;

internal class HttpClientService : IHttpClientService
{
    public HttpResponseMessage Post<TDto>(string url, TDto dto)
    {
        var uri = new Uri(url);
        var client = new HttpClient
        {
            BaseAddress = new Uri(uri.GetLeftPart(UriPartial.Authority))
        };

        var content = new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        var response = client.PostAsync(uri, content).Result;

        return response.EnsureSuccessStatusCode();
    }
}