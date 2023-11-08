using CashManager.Banking.Domain.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost(nameof(Login))]
    public Task<string> Login(string email, string password)
    {

    }
}