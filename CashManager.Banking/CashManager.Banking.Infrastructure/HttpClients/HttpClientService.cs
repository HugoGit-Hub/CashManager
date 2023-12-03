using CashManager.Banking.Domain.HttpClients;
using CashManager.Banking.Domain.Transactions;
using System.Text;
using System.Text.Json;

namespace CashManager.Banking.Infrastructure.HttpClients;

internal class HttpClientService : IHttpClientService
{
    public async Task Validate(Transaction transaction, CancellationToken cancellationToken)
    {
        var uri = new Uri(transaction.Url);
        var client = new HttpClient
        {
            BaseAddress = new Uri(uri.GetLeftPart(UriPartial.Authority))
        };

        var content = new StringContent(JsonSerializer.Serialize(transaction), Encoding.UTF8, "application/json");
        var response = await client.PutAsync(uri, content, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpCallbackRequestException($"Something went wrong during the http callback request : {response.ReasonPhrase}");
        }
    }
}