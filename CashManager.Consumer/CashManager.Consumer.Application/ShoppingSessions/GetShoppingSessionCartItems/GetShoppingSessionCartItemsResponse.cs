using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.ShoppingSessions.GetShoppingSessionCartItems;

public record GetShoppingSessionCartItemsResponse
{
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }
}