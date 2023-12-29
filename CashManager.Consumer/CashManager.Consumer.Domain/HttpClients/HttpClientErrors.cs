using CashManager.Consumer.Domain.ErrorHandling;
using System.Net;

namespace CashManager.Consumer.Domain.HttpClients;

public static class HttpClientErrors
{
    public static Error HttpCallbackRequestError(HttpStatusCode httpStatusCode) => new(
        "HttpClient.CallbackRequest", $"Something went wrong during the http callback request, returned : {httpStatusCode}");

    public static Error HttpCallbackRequestError(HttpContent httpContent) => new(
        "HttpClient.CallbackRequest", $"Something went wrong during the http callback request, returned : {httpContent}");

    public static Error HttpCallbackRequestError(string body) => new(
        "HttpClient.CallbackRequest", $"Something went wrong during the http callback request, returned : {body}");
}