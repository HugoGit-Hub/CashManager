using CashManager.Consumer.Domain.ErrorHandling;
using CashManager.Consumer.Domain.User;

namespace CashManager.Consumer.Application.User;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> IsEmailUnique(string email, CancellationToken cancellationToken)
    {
        var isEmailUnique = await _userRepository.IsEmailUnique(email, cancellationToken);

        return !isEmailUnique
            ? Result.Success()
            : Result.Failure(UserErrors.EmailAlreadyExistError(email));
    }

    public Task Add(Users user, CancellationToken cancellationToken)
    {
        return _userRepository.Add(user, cancellationToken);
    }
}