using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.ShoppingSessions.GetCurrentShoppingSession;

public record GetCurrentShoppingSessionResponse
{
    [Required]
    public double TotalPrice { get; set; }
}