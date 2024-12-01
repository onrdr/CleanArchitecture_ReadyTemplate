namespace ApplicationCore.DTOs.Product;

public class ViewProductDto
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; } 
}
