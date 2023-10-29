using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ProductRepository : BaseRepository<Product> , IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        
    }
}
