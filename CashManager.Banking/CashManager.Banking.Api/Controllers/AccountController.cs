using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Application.Accounts.GetAccountByCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly ISender _sender;

    public AccountController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetAccountsByCurrentUser))]
    public async Task<ActionResult<IEnumerable<AccountResponse>>> GetAccountsByCurrentUser(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAccountsByCurrentUserQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }
}
