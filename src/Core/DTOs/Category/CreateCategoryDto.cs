using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.DTOs.Category;

public class CreateCategoryDto
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }
}
