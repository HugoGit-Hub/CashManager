using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Presentation.Controllers;

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
        try
        {
            var account = await _accountService.Get(cancellationToken);

            return Ok(account.Adapt<IEnumerable<AccountDto>>());
        }
        catch (NullAccountException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
