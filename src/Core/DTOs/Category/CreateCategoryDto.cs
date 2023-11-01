using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Category;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
}
