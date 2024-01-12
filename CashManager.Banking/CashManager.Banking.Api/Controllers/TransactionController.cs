using CashManager.Banking.Application.Transactions.ValidateTransaction;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly ISender _sender;

    public TransactionController(
        ITransactionService transactionService,
        ISender sender)
    {
        _transactionService = transactionService;
        _sender = sender;
    }

    [Authorize(AuthenticationSchemes = "ApiKeyScheme")]
    [HttpPost(nameof(Post))]
    public async Task<ActionResult<TransactionDto>> Post(TransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var result = await _transactionService.SignAndPost(transactionDto.Adapt<Transaction>(), cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Adapt<TransactionDto>());
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetByUserAccounts))]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByUserAccounts(string accountNumber, CancellationToken cancellationToken)
    {
        var result = await _transactionService.GetByUserAccounts(accountNumber, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Adapt<IEnumerable<TransactionDto>>());
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetPendingTransactionsForUser))]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetPendingTransactionsForUser(CancellationToken cancellationToken)
    {
        var result = await _transactionService.GetPendingTransactionsForUser(cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Adapt<IEnumerable<TransactionDto>>());
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut(nameof(ValidateTransaction))]
    public async Task<ActionResult<ValidateTransactionResponse>> ValidateTransaction(ValidateTransactionRequest validateTransactionRequest, CancellationToken cancellationToken)
    {
        var validateCommand = await _sender.Send(new ValidateTransactionCommand(validateTransactionRequest), cancellationToken);
        if (validateCommand.IsFailure)
        {
            return BadRequest(validateCommand.Error);
        }

        return Ok(validateCommand.Value);
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut(nameof(AbortTransaction))]
    public async Task<ActionResult<TransactionDto>> AbortTransaction(TransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var result = await _transactionService.ValidateOrAbort(transactionDto.Adapt<Transaction>(), TransactionStateEnum.Aborted, cancellationToken);
        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result.Value.Adapt<TransactionDto>());

    }
}