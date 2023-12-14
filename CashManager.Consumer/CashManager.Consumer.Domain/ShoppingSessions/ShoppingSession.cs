using CashManager.Consumer.Domain.CartItems;
using CashManager.Consumer.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Domain.ShoppingSessions;

public record ShoppingSession
{
    [Key]
    public int Id { get; set; }

    [Required]
    public double TotalPrice { get; set; }

    [Required]
    public bool State { get; set; }

    public int UserId { get; set; }

    public Users User { get; set; } = null!;

    public ICollection<CartItem> CartItems { get; } = new List<CartItem>();
}