using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.HttpClients;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Presentation.Dto;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CashManager.Banking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly IAccountService _accountService;
    private readonly IHttpClientService _httpClientService;

    public TransactionController(
        ITransactionService transactionService,
        IAccountService accountService,
        IHttpClientService httpClientService)
    {
        _transactionService = transactionService;
        _accountService = accountService;
        _httpClientService = httpClientService;
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
    public async Task<ActionResult<TransactionDto>> ValidateTransaction(TransactionDto transactionDto, CancellationToken cancellationToken)
    {
        var validate = await _transactionService.ValidateOrAbort(transactionDto.Adapt<Transaction>(), TransactionStateEnum.Success, cancellationToken);
        if (validate.IsFailure)
        {
            return BadRequest(validate.Error);
        }

        var transaction = await _accountService.Transaction(validate.Value.Creditor, validate.Value.Debtor, validate.Value.Amount, cancellationToken);
        if (transaction.IsFailure)
        {
            return BadRequest(transaction.Error);
        }

        var putTransaction = await _httpClientService.PutTransaction(transactionDto.Adapt<Transaction>(), cancellationToken);
        if (putTransaction.IsFailure)
        {
            return BadRequest(putTransaction.Error);
        }

        return Ok(validate.Value.Adapt<TransactionDto>());
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