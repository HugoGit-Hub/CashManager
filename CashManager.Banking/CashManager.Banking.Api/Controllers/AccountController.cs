using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(Get))]
    public async Task<ActionResult<IEnumerable<AccountDto>>> Get(CancellationToken cancellationToken)
    {
        var result = await _accountService.Get(cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Adapt<IEnumerable<AccountDto>>());
    }
}
