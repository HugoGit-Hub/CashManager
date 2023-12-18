namespace CashManager.Consumer.Application.CartItems.CreateCartItem;

public record CreateCartItemResponse
{
    public int Quantity { get; set; }

    public string ArticleName { get; set; } = null!;
}