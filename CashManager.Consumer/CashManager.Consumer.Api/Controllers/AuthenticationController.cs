using CashManager.Consumer.Application.Authentication.Register;
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

    [HttpPost]
    public ActionResult<Result<string>> Register(RegisterRequest registerRequest)
    {
        var handler = _sender.Send(new RegisterCommand(registerRequest));
        if (handler.Result.IsFailure)
        {
            return BadRequest(handler.Result.Error);
        }

        return Ok(handler.Result.Value);
    }
}