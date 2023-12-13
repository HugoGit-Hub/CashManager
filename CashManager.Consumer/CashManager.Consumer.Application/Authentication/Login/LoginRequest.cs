using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.Authentication.Login;

public record LoginRequest
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}