using MagyarGravir.Shop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MagyarGravir.Shop.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // seed alap kategóriák
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Készleten lévő termékek", IsCustom = false },
            new Category { Id = 2, Name = "Egyedi pénzcsipeszek", IsCustom = true },
            new Category { Id = 3, Name = "Egyedi öngyújtók", IsCustom = true },
            new Category { Id = 4, Name = "Egyedi medálok", IsCustom = true },
            new Category { Id = 5, Name = "Egyedi kulcstartók", IsCustom = true },
            new Category { Id = 6, Name = "Egyéb egyedi termékek", IsCustom = true }
        );
    }

}
