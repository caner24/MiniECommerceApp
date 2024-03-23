using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Helpers;

namespace MiniECommerceApp.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MiniECommerceApp with Asp.NETCore Minimal APIs",
                    Contact = new OpenApiContact
                    {
                        Email = "cnr24clp@gmail.com",
                        Name = "Caner Ay Celep",
                        Url = new Uri("https://caneraycelep.social")
                    }
                });
            });
        }

        public static void ServiceLifetimeSetup(this IServiceCollection services)
        {
            services.AddScoped<IProductDal, ProductDal>();

            services.AddScoped<ISortHelper<Product>, SortHelper<Product>>();
            services.AddScoped<IDataShaper<Product>, DataShaper<Product>>();

        }
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                );
            });
        }

        public static void IdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("defaultConnection");
            ServerVersion serverVersion = ServerVersion.AutoDetect(connectionString);
            services.AddDbContext<MiniECommerceContext>(options => options.UseMySql(connectionString, serverVersion, b => b.MigrationsAssembly("MiniECommerceApp.WebApi")));
            services.AddIdentityApiEndpoints<User>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
                options.Lockout.MaxFailedAccessAttempts = 3;
            }).AddUserManager<UserManager<User>>().AddRoles<IdentityRole>().AddRoleManager<RoleManager<IdentityRole>>().AddApiEndpoints().AddDefaultTokenProviders().AddEntityFrameworkStores<MiniECommerceContext>();
            services.AddAuthorizationBuilder();
        }
    }
}
