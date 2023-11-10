using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CashManager.Banking.Presentation.Configuration.Schemes;

public class ApiKeyHandler : AuthenticationHandler<ApiKeyOption>
{
    public ApiKeyHandler(
        IOptionsMonitor<ApiKeyOption> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(Options.HeaderField, out var apiKeyHeaderValue))
        {
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
        }

        var providedApiKey = apiKeyHeaderValue.ToString();
        if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != Options.HeaderAttemptedValue)
        {
            return Task.FromResult(AuthenticateResult.Fail("Unauthorized"));
        }

        var identity = new ClaimsIdentity("ApiKey");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "ApiKey");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}