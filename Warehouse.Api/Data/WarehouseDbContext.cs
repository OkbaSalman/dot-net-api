using Microsoft.EntityFrameworkCore;
using Warehouse.Api.Data.Entities;

namespace Warehouse.Api.Data;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {   
    }

    public DbSet<Product> Products => Set<Product>();

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Product>(entity =>
       {
           entity.Property(p => p.Name).HasMaxLength(200).IsRequired();
           entity.Property(p => p.Price).HasPrecision(18, 2);
       });

       modelBuilder.Entity<User>(entity =>
        {
            entity.Property(u => u.Email).HasMaxLength(256).IsRequired();
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).HasMaxLength(32).IsRequired();
        });
    }
}