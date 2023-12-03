using CashManager.Consumer.Domain.Transactions;
using CashManager.Consumer.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Consumer.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpPost(nameof(Post))]
    public async Task<ActionResult<TransactionDto>> Post(TransactionDto transaction, CancellationToken cancellationToken)
    {
        var result = await _transactionService.Post(transaction.Adapt<Transaction>(), cancellationToken);

        return Ok(result.Adapt<TransactionDto>());
    }

    [HttpPut(nameof(Validate))]
    public async Task<ActionResult> Validate(TransactionDto transaction, CancellationToken cancellationToken)
    {
        try
        {
            await _transactionService.Put(transaction.Adapt<Transaction>(), cancellationToken);
            
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
