using CashManager.Consumer.Domain.ErrorHandling;
using MediatR;

namespace CashManager.Consumer.Application.Authentication.Register;

public record RegisterCommand(RegisterRequest RegisterRequest) : IRequest<Result<string>>;