using CashManager.Consumer.Application.Transactions.CreateTransaction;
using CashManager.Consumer.Application.Transactions.CreateTransaction.Requests;
using CashManager.Consumer.Application.Transactions.ValidateTransaction;
using CashManager.Consumer.Domain.Transactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ISender _sender;

    public TransactionController(ISender sender)
    {
        _sender = sender;
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPost(nameof(Create))]
    public async Task<ActionResult> Create(CreateTransactionRequest transactionRequest, CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new CreateTransactionCommand(transactionRequest), cancellationToken);
        if (handler.IsFailure)  
        {
            return BadRequest(handler.Error);
        }

        return Ok();
    }

    [HttpPut(nameof(Validate))]
    public async Task<ActionResult> Validate(ValidateTransactionRequest transaction, CancellationToken cancellationToken)
    {
        var handler = await _sender.Send(new ValidateTransactionCommand(transaction), cancellationToken);
        if (handler.IsFailure)
        {
            return BadRequest(handler.Error);
        }

        return Ok();
    }
}
