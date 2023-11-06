using ApplicationCore.Entities;
using Infrastructure.Data.Seeders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Seed();
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
