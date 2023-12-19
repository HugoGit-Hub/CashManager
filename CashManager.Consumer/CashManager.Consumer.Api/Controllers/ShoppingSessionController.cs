using CashManager.Consumer.Application.ShoppingSessions.GetCurrentOpenedShoppingSessionCartItems;
using CashManager.Consumer.Application.ShoppingSessions.GetCurrentShoppingSession;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingSessionController : ControllerBase
{
    private readonly ISender _sender;

    public ShoppingSessionController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetCurrentOpenedShoppingSessionCartItems))]
    public async Task<ActionResult<IEnumerable<GetShoppingSessionCartItemsResponse>>> GetCurrentOpenedShoppingSessionCartItems(CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new GetShoppingSessionCartItemsQuery(), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetCurrentShoppingSession))]
    public async Task<ActionResult<GetCurrentShoppingSessionResponse>> GetCurrentShoppingSession(CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new GetCurrentShoppingSessionQuery(), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }
}