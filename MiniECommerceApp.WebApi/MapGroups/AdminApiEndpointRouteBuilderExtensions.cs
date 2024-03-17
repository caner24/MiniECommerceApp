using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Entity.DTOs;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class AdminApiEndpointRouteBuilderExtensions
    {
        public static void MapAdminApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/", Get);
            endpoints.MapGet("/addProduct", AddProduct);
        }
        private static IResult Get()
        {
            return Results.Content(" This is admin page !.");
        }

        private static IResult AddProduct([FromForm] AddProductDto addProductDto)
        {
            return Results.Content(" This is admin page !.");
        }
    }
}
