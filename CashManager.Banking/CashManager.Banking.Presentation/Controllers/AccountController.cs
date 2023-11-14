using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Presentation.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(Get))]
    public async Task<AccountDto> Get(CancellationToken cancellationToken)
    {
        var account = await _accountService.Get(cancellationToken);

        return account.Adapt<AccountDto>();
    }
}


