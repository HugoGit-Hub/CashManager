﻿using CashManager.Consumer.Domain.Articles;
using CashManager.Consumer.Domain.ShoppingSessions;
using CashManager.Consumer.Domain.Transactions;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Domain.User;

public record Users
{
    [Key]
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
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public ICollection<Transaction> Transactions { get; } = new List<Transaction>();

    public ICollection<ShoppingSession> ShoppingSessions { get; } = new List<ShoppingSession>();

    public ICollection<Article> Articles { get; } = new List<Article>();
}