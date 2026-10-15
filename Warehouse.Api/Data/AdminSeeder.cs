using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Warehouse.Api.Data.Entities;

namespace Warehouse.Api.Data;

public static class AdminSeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<WarehouseDbContext>();
        var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();

        var email = config["Admin:Email"]!.Trim().ToLowerInvariant();
        var exists = await db.Users.AnyAsync(u => u.Email == email);
        if (exists)
            return;

        var admin = new User
        {
            Email = email,
            Role = "Admin"
        };
        var hasher = new PasswordHasher<User>();
        admin.PasswordHash = hasher.HashPassword(admin, config["Admin:Password"]!);

        db.Users.Add(admin);
        await db.SaveChangesAsync();
    }
}