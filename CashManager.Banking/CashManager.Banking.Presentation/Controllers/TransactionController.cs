using CashManager.Banking.Application.Accounts;
using CashManager.Banking.Application.Transactions;
using CashManager.Banking.Domain.Accounts;
using CashManager.Banking.Domain.HttpClients;
using CashManager.Banking.Domain.Transactions;
using CashManager.Banking.Infrastructure.CurrentUser;
using CashManager.Banking.Infrastructure.HttpClients;
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
    public async Task<ActionResult<TransactionDto>> Post(TransactionDto transactionDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _transactionService.SignAndPost(transactionDto.Adapt<Transaction>(), cancellationToken);

            return Ok(result.Adapt<TransactionDto>());
        }
        catch (Exception ex) when (ex is BadTransactionStateException or UserAccountNotFoundException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetByUserAccounts))]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetByUserAccounts(string accountNumber,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _transactionService.GetByUserAccounts(accountNumber, cancellationToken);

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

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut(nameof(ValidateTransaction))]
    public async Task<ActionResult<TransactionDto>> ValidateTransaction(TransactionDto transactionDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validate = await _transactionService.Validate(transactionDto.Adapt<Transaction>(), cancellationToken);
            await _accountService.Transaction(validate.Creditor, validate.Debtor, validate.Amount, cancellationToken);
            await _httpClientService.Post(transactionDto.Adapt<Transaction>(), cancellationToken);

            return Ok(validate.Adapt<TransactionDto>());
        }
        catch (Exception ex) when (
            ex is NullTransactionException
                or NullAccountException
                or WrongSignatureException
                or NonDebatableAccountException
                or HttpCallbackRequestException
                or UriFormatException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpGet(nameof(GetPendingTransactionsForUser))]
    public async Task<ActionResult<IEnumerable<TransactionDto>>> GetPendingTransactionsForUser(
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _transactionService.GetPendingTransactionsForUser(cancellationToken);

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

    [Authorize(AuthenticationSchemes = "Bearer")]
    [HttpPut(nameof(AbortTransaction))]
    public async Task<ActionResult<TransactionDto>> AbortTransaction(TransactionDto transactionDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var validate = await _transactionService.Abort(transactionDto.Adapt<Transaction>(), cancellationToken);

            return Ok(validate.Adapt<TransactionDto>());
        }
        catch (Exception ex) when (
            ex is NullTransactionException
                or NullAccountException
                or WrongSignatureException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}