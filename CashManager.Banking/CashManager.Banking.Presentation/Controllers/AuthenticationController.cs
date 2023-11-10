using System.ComponentModel.DataAnnotations;
using CashManager.Banking.Domain.Authentication;
using CashManager.Banking.Domain.User;
using CashManager.Banking.Infrastructure.Authentication;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authorization;

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

    [AllowAnonymous]
    [HttpPost(nameof(Login))]
    public async Task<ActionResult<string>> Login(
        [DataType(DataType.EmailAddress)] string email,
        [DataType(DataType.Password)] string password, 
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authenticationService.Login(email, password, cancellationToken);
            return Ok(result);
        }
        catch (InvalidCredentialException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [AllowAnonymous]
    [HttpPost(nameof(Register))]
    public async Task<ActionResult<UserDto>> Register(UserDto userDto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _authenticationService.Register(userDto.Adapt<Users>(), cancellationToken);
            return Ok(result.Adapt<UserDto>());
        }
        catch (ExistingEmailException)
        {
            return Unauthorized();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}