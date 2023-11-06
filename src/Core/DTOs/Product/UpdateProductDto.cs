using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.DTOs.Product;

public class UpdateProductDto
{
    [Required]
    public Guid Id { get; set; }

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
