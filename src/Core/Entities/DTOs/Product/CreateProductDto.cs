using System.ComponentModel.DataAnnotations;

namespace Core.Entities.DTOs.Product;

public class CreateProductDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}
