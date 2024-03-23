using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Queries.Request;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class ProductApiEndpointRouteBuilderExtensions
    {

        public static void MapProductApi(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/getAllProduct", GetAllProduct);
            endpointRouteBuilder.MapPost("/addProduct", AddProduct);
        }
        private static async Task<IResult> GetAllProduct(IMediator mediator, [AsParameters] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            var response = await mediator.Send(getAllProductQueryRequest);
            var metadata = new
            {
                response.TotalCount,
                response.PageSize,
                response.CurrentPage,
                response.TotalPages,
                response.HasNext,
                response.HasPrevious
            };
            //Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Results.Ok(metadata);
        }
        private static IResult GetProductById()
        {
            return Results.Ok();
        }
        private static async Task<IResult> AddProduct(IMediator mediator, AddProductCommandRequest addProductCommandRequest)
        {
            var addedProduct = await mediator.Send(addProductCommandRequest);
            return Results.Ok(addedProduct);
        }
    }
}
