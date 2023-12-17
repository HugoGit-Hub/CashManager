using System.ComponentModel.DataAnnotations;

namespace CashManager.Consumer.Application.Articles;

public record ArticleResponse
{
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
}