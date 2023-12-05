using CashManager.Banking.Domain.User;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetCurrentUser))]
    public async Task<ActionResult<UserDto>> GetCurrentUser(CancellationToken cancellationToken)
    {
        var result = await _userService.Get(cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Adapt<UserDto>());
    }
}