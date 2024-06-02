using IdentityManager.Data;
using IdentityManager.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityManager.Extensions;

public static class SeedConfigurationExtension
{
    public async static Task SeedDBData(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await SeedData.Initialize(services, userManager);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }
        }
    }
}
