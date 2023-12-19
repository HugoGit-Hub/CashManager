using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.CartItems.CreateCartItem;

public record CreateCartItemRequest
{
    [Required]
    public int Quantity { get; set; }

    [Required]
    public int IdArticle { get; set; }
}