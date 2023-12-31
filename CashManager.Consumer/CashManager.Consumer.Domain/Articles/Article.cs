﻿using CashManager.Consumer.Domain.User;
using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Domain.Articles;

public class Article
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    [Required]
    public double Price { get; set; }

    [MaxLength(512)]
    public string? Description { get; set; }

    [MaxLength(512)]
    [DataType(DataType.ImageUrl)]
    public string? ImageUrl { get; set; }

    public ICollection<Users> Users { get; } = new List<Users>();
}
