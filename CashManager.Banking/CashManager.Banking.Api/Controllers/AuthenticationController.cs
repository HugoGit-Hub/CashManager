using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.User;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    public async Task<ActionResult<string>> Login(
        [DataType(DataType.EmailAddress)] string email,
        [DataType(DataType.Password)] string password,
        CancellationToken cancellationToken)
    {
        var result = await _authenticationService.Login(email, password, cancellationToken);
        if (result.Error == AuthenticationErrors.InvalidCredentialsError)
        {
            return Unauthorized();
        }

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value);
    }

    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public async Task<ActionResult<UserDto>> Register(UserDto userDto, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.Register(userDto.Adapt<Users>(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }
        
        return Ok(result.Value.Adapt<UserDto>());
    }
}