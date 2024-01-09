using System.ComponentModel.DataAnnotations;

namespace CashManager.Banking.Presentation.Dto;

public record LoginDto
{
    [Required] 
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}