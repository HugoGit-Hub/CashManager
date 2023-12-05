using Microsoft.AspNetCore.Authentication;

namespace CashManager.Banking.Api.Configuration.Schemes;

public class ApiKeyOption : AuthenticationSchemeOptions
{
    public string HeaderField { get; set; } = null!;

    public string HeaderAttemptedValue { get; set; } = null!;
}