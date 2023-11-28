using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashManager.Consumer.Domain.Articles;

public class Article
{
    [Key]
    [Required]
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
