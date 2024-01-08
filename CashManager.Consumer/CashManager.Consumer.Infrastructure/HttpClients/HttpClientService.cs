using CashManager.Consumer.Application.HttpClients;
using CashManager.Consumer.Application.Transactions.CreateTransaction.Requests;
using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.HttpClients;
using System.Text;
using System.Text.Json;

namespace CashManager.Consumer.Infrastructure.HttpClients;

internal class HttpClientService : IHttpClientService
{
    public async Task<Result> PostTransaction(CreateBankingTransactionRequest createBankingTransactionRequest, CancellationToken cancellationToken)
    {
        var uri = new Uri("https://vh71wppn-5000.uks1.devtunnels.ms/api/Transaction/Post");
        var client = new HttpClient
        {
            BaseAddress = new Uri(uri.GetLeftPart(UriPartial.Authority))
        };
        client.DefaultRequestHeaders.Add("ApiKey", ",.PjqV#.|X>kgp{?JsygExquC;tVuf5%");


        var content = new StringContent(JsonSerializer.Serialize(createBankingTransactionRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(uri, content, cancellationToken);

        return response.IsSuccessStatusCode
            ? Result.Success()
            : Result.Failure(HttpClientErrors.HttpCallbackRequestError(await response.Content.ReadAsStringAsync(cancellationToken)));
    }
}