﻿using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.ShoppingSessions.GetCurrentOpenedShoppingSessionCartItems;

public record GetShoppingSessionCartItemsResponse
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string ArticleName { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public double TotalArticlePrice { get; set; }

    [Required]
    public string? ImageUrl { get; set; }

    [Required]
    public int ArticleId { get; set; }
}