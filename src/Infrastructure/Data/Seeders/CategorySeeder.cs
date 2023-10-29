using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Seeders;

public static class CategorySeeder
{
    public static ModelBuilder SeedCategories(this ModelBuilder modelBuilder)
    {
        var categories = GetCategories();

        foreach (var category in categories)
        {
            modelBuilder.Entity<Category>()
                .HasData(category);
        }

        return modelBuilder;
    }

    private static IEnumerable<Category> GetCategories()
    {
        return new List<Category>()
        {
            new()
            {
                Id = Guid.Parse("81e4e565-7bea-4f4f-816a-def22c28f42f"),
                Name = "Category Name 1",
                Description = "Category 1 Description",
            },

            new()
            {
                Id = Guid.Parse("15813940-b20f-48c4-af36-c3375d69339d"),
                Name = "Category Name 2",
                Description = "Category 2 Description",
            }
        };
    }
}
