using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MiniECommerceApp.WebApi.Extensions
{
    public static class MapGroupExtensions
    {
        public static void MapProductApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/getAllProducts", Get);
            endpoints.MapGet("/getProductsWithId/{id}", GetProductsWithId);
        }

        private static IResult Get()
        {
            return Results.Ok("Hello World!");
        }
        private static IResult GetProductsWithId([FromRoute] int id)
        {
            return Results.Ok(id);
        }

    }
}
