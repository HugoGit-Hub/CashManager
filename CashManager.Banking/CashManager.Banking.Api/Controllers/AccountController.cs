using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Presentation.Dto;
using Mapster;
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
    [HttpGet(nameof(Get))]
    public async Task<ActionResult<GetAccountsByCurrentUserResponse>> Get(CancellationToken cancellationToken)
    {
        var result = await _sender.Send(new GetAccountsByCurrentUserQuery(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Adapt<IEnumerable<AccountDto>>());
    }
}
