using Microsoft.OpenApi.Models;

namespace MiniECommerceApp.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
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
    }
}
