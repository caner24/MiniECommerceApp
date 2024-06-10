using Microsoft.AspNetCore.Identity;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using Stripe;

namespace MiniECommerceApp.WebApi.SeedData
{
    public static class AdminUser
    {
        public static async Task IsAdminUserExist(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var context = scope.ServiceProvider.GetRequiredService<MiniECommerceContext>();
            var stripeClient = scope.ServiceProvider.GetRequiredService<StripeClient>();
            var adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var adminUser = await userManager.FindByNameAsync(app.Configuration["AdminUser:Email"]);
            if (adminUser == null)
            {
                var newUser = new User { UserName = app.Configuration["AdminUser:Email"], Email = app.Configuration["AdminUser:Email"] };
                var options = new CustomerCreateOptions
                {
                    Email = newUser.Email,
                    Name = newUser.UserName,
                };
                var service = new CustomerService(stripeClient);
                var customer = await service.CreateAsync(options);
                newUser.StripeUserId = customer.Id;
                var result = await userManager.CreateAsync(newUser, app.Configuration["AdminUser:Password"]);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Admin");
                }
            }
        }
    }
}
