using ApplicationCore.Interfaces.Entities;
using ApplicationCore.Interfaces.Entities.Base;

namespace ApplicationCore.Entities;

public class Category : IBaseEntity, ICategory
{
    public Category()
    {
        Id = Guid.NewGuid();
        Products = new HashSet<Product>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
