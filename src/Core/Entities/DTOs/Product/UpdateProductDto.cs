using System.ComponentModel.DataAnnotations;

namespace Core.Entities.DTOs.Product;

public class UpdateProductDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
}
