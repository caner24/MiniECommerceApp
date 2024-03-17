using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using MiniECommerceApp.Data.Concrete;

namespace MiniECommerceApp.WebApi.DesignTime
{
    public static class SeedContext
    {

        public static async void MigrateDb(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<MiniECommerceContext>();

                if (!await context.Database.CanConnectAsync())
                {
                    await context.Database.MigrateAsync();
                }
            }
        }
    }
}
