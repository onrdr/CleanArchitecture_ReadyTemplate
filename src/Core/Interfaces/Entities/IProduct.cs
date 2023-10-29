namespace Core.Interfaces.Entities;

public interface IProduct
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
