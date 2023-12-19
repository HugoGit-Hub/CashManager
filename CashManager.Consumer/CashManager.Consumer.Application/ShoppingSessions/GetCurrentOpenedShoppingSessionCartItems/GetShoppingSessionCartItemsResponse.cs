using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.ShoppingSessions.GetCurrentOpenedShoppingSessionCartItems;

public record GetShoppingSessionCartItemsResponse
{
    [Required]
    public string ArticleName { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }
}