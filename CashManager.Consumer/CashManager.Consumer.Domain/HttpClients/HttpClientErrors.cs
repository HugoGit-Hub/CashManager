﻿using CashManager.Consumer.Domain.ErrorHandling;

namespace CashManager.Consumer.Domain.HttpClients;

public static class HttpClientErrors
{
    public static readonly Error HttpCallbackRequestError = new(
        "HttpClient.CallbackRequest", "Something went wrong during the http callback request");
}