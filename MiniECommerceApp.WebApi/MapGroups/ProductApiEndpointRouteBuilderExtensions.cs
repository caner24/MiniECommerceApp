using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Queries.Request;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Exceptions;
using MiniECommerceApp.Entity.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class ProductApiEndpointRouteBuilderExtensions
    {

        public static void MapProductApi(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/getAllProduct", GetAllProduct);

            endpointRouteBuilder.MapGet("/getProductById/{id}", GetProductById);
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
            return Results.Ok(response);
        }
        private static async Task<IResult> GetProductById(IProductDal productDal, [FromRoute] int Id)
        {
            var product = await productDal.Get(x => x.Id == Id).Include(x=>x.Categories).Include(x=>x.ProductDetail).FirstOrDefaultAsync();
            if (product is not null)
            {
                return Results.Ok(product);
            }
            throw new ProductNotFoundException();

        }
    }
}
