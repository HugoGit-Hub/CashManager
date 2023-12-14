using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.Authentication.Register;

public record RegisterRequest
{
    [Required]
    [DataType(DataType.Text)]
    public string Firstname { get; set; } = null!;

    [Required]
    [DataType(DataType.Text)]
    public string Lastname { get; set; } = null!;

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}