using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.Authentication.Login;

public record LoginQuery(LoginRequest LoginRequest) : IRequest<Result<string>>;