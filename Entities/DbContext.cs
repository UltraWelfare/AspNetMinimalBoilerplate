using Microsoft.EntityFrameworkCore;

namespace AspNetMinimalBoilerplate.Entities;

public class OrderContext: DbContext
{
    public DbSet<Item> Items { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source=orders.db");
}