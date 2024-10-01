using Microsoft.EntityFrameworkCore;
using POT_semestralka_backend.Models;

namespace POT_semestralka_backend.Database;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
    {
    }
    
    public DbSet<Hotel> Hotels { get; init; }
}