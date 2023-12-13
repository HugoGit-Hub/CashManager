using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.ShoppingSessions;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Domain.CartItems;

public record CartItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Quantity { get; set; }

    public int IdArticle { get; set; }

    public int IdShoppingSession { get; set; }

    public Article Article { get; set; } = null!;

    public ShoppingSession ShoppingSession { get; set; } = null!;
}
