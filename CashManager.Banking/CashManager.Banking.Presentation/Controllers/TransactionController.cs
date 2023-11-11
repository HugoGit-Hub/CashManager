using CashManager.Banking.Application.Transactions;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Infrastructure.CurrentUser;
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
        try
        {
            var result = await _transactionService.SignAndPost(transactionDto.Adapt<Transaction>(), cancellationToken);

            return Ok(result.Adapt<TransactionDto>());
        }
        catch (BadTransactionStateException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetByUser))]
    public async Task<ActionResult<TransactionDto>> GetByUser(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _transactionService.GetByUser(cancellationToken);
        
            return Ok(result.Adapt<IEnumerable<TransactionDto>>());
        }
        catch (ClaimTypeNullException ex)
        {
            return Forbid(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }

    }   
}