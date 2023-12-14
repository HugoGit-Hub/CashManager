using CashManager.Consumer.Application.Authentication.Login;
using CashManager.Consumer.Application.Authentication.Register;
using CashManager.Consumer.Domain.Authentication;
using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly ISender _sender;

    public AuthenticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost(nameof(Register))]
    public async Task<ActionResult<Result<string>>> Register(RegisterRequest registerRequest)
    {
        var handler = await _sender.Send(new RegisterCommand(registerRequest));
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }

    [HttpPost(nameof(Login))]
    public async Task<ActionResult<Result<string>>> Login(LoginRequest loginRequest)
    {
        var handler = await _sender.Send(new LoginQuery(loginRequest));
        if (handler.Error == AuthenticationErrors.WrongCredentialsError)
        {
            return Unauthorized(handler.Error);
        }

        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }
}