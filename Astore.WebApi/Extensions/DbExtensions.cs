using Astore.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Astore.WebApi.Extensions;

public static class DbExtensions
{
    public static void AddDatabase(this IServiceCollection services, ConfigurationManager configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<StoreDbContext>(options =>
            options.UseSqlite(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();
    }

    public static async Task MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        using (var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>())
            await context.Database.MigrateAsync();

        using (var scope = app.Services.CreateScope())
        using (var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>())
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole("Admin");
                await roleManager.CreateAsync(adminRole);
            }
        }
    }
}