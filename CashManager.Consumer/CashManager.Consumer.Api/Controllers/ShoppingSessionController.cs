using CashManager.Consumer.Application.ShoppingSessions.DeleteCartItemFromCurrentShoppingSession;
using CashManager.Consumer.Application.ShoppingSessions.GetCurrentOpenedShoppingSessionCartItems;
using CashManager.Consumer.Application.ShoppingSessions.GetCurrentShoppingSession;
using CashManager.Consumer.Application.ShoppingSessions.UpdateCartItemFromCurrentShoppingSession;
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

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpDelete(nameof(DeleteCartItemFromCurrentShoppingSession))]
    public async Task<ActionResult>
        DeleteCartItemFromCurrentShoppingSession(int cartItemId, CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new DeleteCartItemFromCurrentShoppingSessionCommand(cartItemId), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok();
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut(nameof(UpdateCartItemFromCurrentShoppingSession))]
    public async Task<ActionResult> UpdateCartItemFromCurrentShoppingSession(int cartItemId, int quantity, CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new UpdateCartItemFromCurrentShoppingSessionCommand(cartItemId, quantity), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok();
    }
}