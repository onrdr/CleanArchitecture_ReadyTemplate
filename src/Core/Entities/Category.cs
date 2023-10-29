using Core.Interfaces.Entities;
using Core.Interfaces.Entities.Base;

namespace Core.Entities;

public class Category : IBaseEntity, ICategory
{
    public Category()
    {
        Id = Guid.NewGuid();
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public IEnumerable<Product> Products { get; set; }
}
