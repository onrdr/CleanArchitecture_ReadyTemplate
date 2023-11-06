using ApplicationCore.Interfaces.Entities;
using ApplicationCore.Interfaces.Entities.Base;
using System.Text.Json.Serialization;

namespace ApplicationCore.Entities;

public class Product : IBaseEntity, IProduct
{
    public Product()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }

    [JsonIgnore]
    public Category Category { get; set; }
    public Guid CategoryId { get; set; }
}
