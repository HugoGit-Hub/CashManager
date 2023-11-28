using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Presentation.Dto;

public record UserDto
{
    public int Id { get; set; }

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
    public int Bank { get; set; }
}