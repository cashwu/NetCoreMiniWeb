using Microsoft.EntityFrameworkCore;

namespace MiniWeb;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<People> People { get; set; }
}