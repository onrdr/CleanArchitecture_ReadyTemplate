using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Base;

namespace Infrastructure.Repositories;

public class ProductRepository(ApplicationDbContext context) : BaseRepository<Product>(context) , IProductRepository
{
}
