using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [Authorize(AuthenticationSchemes = "ApiKeyScheme")]
    [HttpPost(nameof(Post))]
    public async Task<ActionResult<TransactionDto>> Post(TransactionDto transactionDto, CancellationToken cancellationToken)
    {

        var result = await _transactionService.Post(transactionDto.Adapt<Transaction>(), cancellationToken);

        return Ok(result);
    }

    [Authorize]
    [HttpGet(nameof(GetAll))]
    public async Task<ActionResult<TransactionDto>> GetAll(int userId, CancellationToken cancellationToken)
    {
        var result = await _transactionService.GetAll(userId, cancellationToken);

        return Ok(result.Adapt<IEnumerable<TransactionDto>>());
    }   
}