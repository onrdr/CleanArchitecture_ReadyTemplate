using Core.Interfaces.Entities;
using Core.Interfaces.Entities.Base;
using System.Text.Json.Serialization;

namespace Core.Entities;

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
