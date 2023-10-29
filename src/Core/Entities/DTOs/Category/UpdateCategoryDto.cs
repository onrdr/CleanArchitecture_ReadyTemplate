using System.ComponentModel.DataAnnotations;

namespace Core.Entities.DTOs.Category;

public class UpdateCategoryDto
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
}
