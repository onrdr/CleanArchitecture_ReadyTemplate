using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class ProductSeeder
{
    public static ModelBuilder SeedProducts(this ModelBuilder modelBuilder)
    {
        var products = GetProducts();

        foreach (var product in products)
        {
            modelBuilder.Entity<Product>()
                .HasData(product);
        }

        return modelBuilder;
    }

    private static List<Product> GetProducts()
    {
        return
        [
            new()
            {
                Id = Guid.Parse("8ff2a6b2-7f3d-4b5f-8069-821f8765a3dd"),
                Name = "Product 1 Name",
                Description = "Product 1 Description",
                Price = 10,
                CategoryId = Guid.Parse("15813940-b20f-48c4-af36-c3375d69339d")
            },
            new()
            {
                Id = Guid.Parse("5dc9ba5e-53c0-4166-87de-5f6f57021256"),
                Name = "Product 2 Name",
                Description = "Product 2 Description",
                Price = 20,
                CategoryId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f")
            },
            new()
            {
                Id = Guid.Parse("25e7bfec-7c34-4081-bbd7-e7c90e13bd27"),
                Name = "Product 3 Name",
                Description = "Product 3 Description",
                Price = 30,
                CategoryId = Guid.Parse("15813940-b20f-48c4-af36-c3375d69339d")
            },
            new()
            {
                Id = Guid.Parse("d57b60ef-449f-4b86-bfa7-9102bf93dff6"),
                Name = "Product 4 Name",
                Description = "Product 4 Description",
                Price = 40,
                CategoryId = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f")
            }
        ];
    }
}
