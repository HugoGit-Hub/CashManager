﻿using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.HttpClients;
using CashManager.Consumer.Domain.Transactions;
using System.Text;
using System.Text.Json;

namespace CashManager.Consumer.Infrastructure.HttpClients;

internal class HttpClientService : IHttpClientService
{
    public async Task<Result> Post(Transaction transaction, CancellationToken cancellationToken)
    {
        var uri = new Uri("https://localhost:7154/api/Transaction/Post");
        var client = new HttpClient
        {
            BaseAddress = new Uri(uri.GetLeftPart(UriPartial.Authority))
        };
        client.DefaultRequestHeaders.Add("ApiKey", ",.PjqV#.|X>kgp{?JsygExquC;tVuf5%");

        var content = new StringContent(JsonSerializer.Serialize(transaction), Encoding.UTF8, "application/json");
        var response = await client.PostAsync(uri, content, cancellationToken);
        
        return !response.IsSuccessStatusCode 
            ? Result.Failure(HttpClientErrors.HttpCallbackRequestError) 
            : Result.Success();
    }
}