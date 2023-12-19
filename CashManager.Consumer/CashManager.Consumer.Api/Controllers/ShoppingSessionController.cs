using CashManager.Consumer.Application.ShoppingSessions.CreateShoppingSession;
using CashManager.Consumer.Application.ShoppingSessions.GetShoppingSessionCartItems;
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
    [HttpGet(nameof(GetShoppingSessionCartItems))]
    public async Task<ActionResult<IEnumerable<GetShoppingSessionCartItemsResponse>>> GetShoppingSessionCartItems(
        int id, 
        CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new GetShoppingSessionCartItemsQuery(id), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok(handler.Value);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost(nameof(CreateShoppingSession))]
    public async Task<ActionResult> CreateShoppingSession(CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new CreateShoppingSessionCommand(), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok();
    }
}