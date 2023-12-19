using CashManager.Consumer.Application.CartItems.CreateCartItem;
using CashManager.Consumer.Domain.CartItems;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartItemController : ControllerBase
{
    private readonly ISender _sender;

    public CartItemController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost(nameof(CreateCartItem))]
    public async Task<ActionResult<CreateCartItemResponse>> CreateCartItem(
        CreateCartItemRequest createCartItemRequest,
        CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new CreateCartItemCommand(createCartItemRequest), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }
}