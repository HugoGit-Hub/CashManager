using System.Net;
using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.HttpClients;

public static class HttpClientErrors
{
    public static Error HttpCallbackRequestError(HttpStatusCode httpStatusCode) => new(
        "HttpClient.CallbackRequest", $"Something went wrong during the http callback request, returned : {httpStatusCode}");
}