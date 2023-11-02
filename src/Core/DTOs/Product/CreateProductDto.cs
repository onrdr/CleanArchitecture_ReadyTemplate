using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Product;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, double.MaxValue)]
    public double Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}
