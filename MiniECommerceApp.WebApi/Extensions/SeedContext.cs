using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using MiniECommerceApp.Data.Concrete;

namespace MiniECommerceApp.WebApi.Extensions
{
    public static class SeedContext
    {

        public static async void MigrateDb(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<MiniECommerceContext>();
                    if (!context.Database.GetAppliedMigrations().Any())
                    {
                        await context.Database.MigrateAsync();
                        Console.WriteLine("Database migration applied successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No pending migrations.");
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
