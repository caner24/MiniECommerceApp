using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Business.Abstract;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.DTOs;
using MiniECommerceApp.Entity.Parameters;
using Newtonsoft.Json;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class ProductApiEndpointRouteBuilderExtensions
    {
        public static void MapProductApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/getAllProducts", GetAll);
            endpoints.MapGet("/getProductsWithId/{id}", GetProductsWithId);
            endpoints.MapPost("/addProduct", AddProduct);
        }
        private static IResult GetAll(HttpContext context, IServiceProvider services, [AsParameters] ProductParameters parameters)
        {
            var productService = services.GetRequiredService<IProductService>();
            var products = productService.GetAll(parameters);

            var metadata = new
            {
                products.TotalCount,
                products.PageSize,
                products.CurrentPage,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };
            context.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Results.Ok(products);
        }
        private static IResult GetProductsWithId([FromRoute] int id)
        {
            return Results.Ok(id);
        }
        private static async Task<IResult> AddProduct(IMapper mapper, IServiceProvider services, [FromBody] AddProductDto addProductDto)
        {
            var productService = services.GetRequiredService<IProductService>();
            var mapperService = services.GetRequiredService<IMapper>();

            var product = mapperService.Map<Product>(addProductDto);
            var returnedProduct = await productService.Add(product);

            return Results.Ok();
        }
    }
}
