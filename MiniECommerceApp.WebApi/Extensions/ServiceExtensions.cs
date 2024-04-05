﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MiniECommerceApp.Core.CrosssCuttingConcerns.Caching;
using MiniECommerceApp.Core.CrosssCuttingConcerns.MailService;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Helpers;
using MiniECommerceApp.WebApi.Mail;
using System.Threading.RateLimiting;

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

        public static void AddRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                        RateLimitPartition.GetFixedWindowLimiter(
                            partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                            factory: partition => new FixedWindowRateLimiterOptions
                            {
                                AutoReplenishment = true,
                                PermitLimit = 5,
                                QueueLimit = 2,
                                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                                Window = TimeSpan.FromMinutes(1)
                            }));
                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.StatusCode = 429;
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        await context.HttpContext.Response.WriteAsync(
                            $"Çok fazla istekde bulundunuz. Lütfen sonra tekrar deneyin {retryAfter.TotalMinutes} dakika. ", cancellationToken: token);
                    }
                    else
                    {
                        await context.HttpContext.Response.WriteAsync(
                            "Çok fazla istekde bulundunuz. Lütfen sonra tekrar deneyin. ", cancellationToken: token);
                    }
                };
            });
        }


        public static void ServiceLifetimeSetup(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductDal, ProductDal>();
            services.AddScoped<ICategoryDal, CategoryDal>();
            services.AddScoped<IBasketDal, BasketDal>();
            services.AddScoped<IInvoicesDal, InvoiceDal>();

            services.AddSingleton<IMessageProducer, RabbitMQProducer>();
            services.AddScoped<ISortHelper<Product>, SortHelper<Product>>();
            services.AddScoped<IDataShaper<Product>, DataShaper<Product>>();

            var emailConfig = configuration
         .GetSection("EmailConfiguration")
         .Get<EmailConfiguration>();
            services.AddSingleton(emailConfig);
            services.AddSingleton<IEmailSender, EmailSender>();
            services.AddSingleton<IEmailSender<User>, MailSender>();
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

        public static void RedisCacheSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:DefaultConnection"];
            });
            services.AddSingleton<RedisCacheService>();

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
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
     opt.TokenLifespan = TimeSpan.FromHours(2));
        }
    }
}
