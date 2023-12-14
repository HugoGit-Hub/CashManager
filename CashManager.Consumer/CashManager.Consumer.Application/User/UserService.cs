using CashManager.Consumer.Domain.Authentication;
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

    public async Task<Result> AreCredentialsCorrects(string email, string password, CancellationToken cancellationToken)
    {
        var areCredentialsCorrects = await _userRepository.AreCredentialsCorrects(email, password, cancellationToken);
        
        return areCredentialsCorrects 
            ? Result.Success()
            : Result.Failure(AuthenticationErrors.WrongCredentialsError);
    }

    public async Task<Result<Users>> GetUserByEmail(string email, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByEmail(email, cancellationToken);
        
        return user is not null
            ? Result<Users>.Success(user)
            : Result<Users>.Failure(UserErrors.NotFoundError);
    }
}