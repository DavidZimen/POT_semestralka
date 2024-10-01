using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using POT_semestralka_backend.Models;

namespace POT_semestralka_backend.Database;

public sealed class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products { get; init; }
}