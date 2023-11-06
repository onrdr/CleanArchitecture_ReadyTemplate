namespace ApplicationCore.DTOs.Category;

public class ViewCategoryWithProductsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Entities.Product> Products { get; set; }
}
