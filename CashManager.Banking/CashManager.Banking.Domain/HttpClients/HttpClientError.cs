using CashManager.Banking.Domain.ErrorHandling;

namespace CashManager.Banking.Domain.HttpClients;

public static class HttpClientError
{
    public static Error HttpCallbackRequestError(string? response) => new(
        "HttpClient.HttpCallbackRequest", $"Something went wrong during the http callback request : {response}");
}