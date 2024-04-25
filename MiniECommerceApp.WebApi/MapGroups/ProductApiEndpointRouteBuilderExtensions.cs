using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Queries.Request;
using MiniECommerceApp.Core.CrosssCuttingConcerns.Caching;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Exceptions;
using MiniECommerceApp.Entity.Helpers;
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

            endpointRouteBuilder.MapGet("/getAllCategories", GetAllCategories);

            endpointRouteBuilder.MapGet("/getProductById/{id}", GetProductById);
        }
        private static async Task<IResult> GetAllProduct([FromServices] RedisCacheService cache, HttpContext context, IMediator mediator, [AsParameters] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            var cachedData = cache?.GetCachedData<PagedList<MiniECommerceApp.Entity.Models.Entity>>("productCache");
            var cachedHeader = cache?.GetCachedData<PagedData>("headerCache");
            if (cachedData is null)
            {
                var response = await mediator.Send(getAllProductQueryRequest);
                if (response != null)
                {
                    var metadataResponse = new PagedData
                    {
                        TotalCount = response.TotalCount,
                        PageSize = response.PageSize,
                        CurrentPage = response.CurrentPage,
                        TotalPages = response.TotalPages,
                        HasNext = response.HasNext,
                        HasPrevious = response.HasPrevious
                    };
                    cache?.SetCachedData("productCache", response, TimeSpan.FromSeconds(60));
                    cache?.SetCachedData("headerCache", metadataResponse, TimeSpan.FromSeconds(60));
                    context.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadataResponse));
                    return Results.Ok(response);
                }
                else
                {
                    return Results.NotFound("No products found.");
                }
            }
            context.Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(cachedHeader));
            return Results.Ok(cachedData);
        }


        private static async Task<IResult> GetAllCategories([FromServices] ICategoryDal categoryDal)
        {
            var categories = await categoryDal.GetAll().ToListAsync();
            return Results.Ok(categories);
        }

        private static async Task<IResult> GetProductById([FromServices] IProductDal productDal, [FromRoute] int Id)
        {
            var product = await productDal.Get(x => x.Id == Id).Include(x => x.Categories).Include(x => x.ProductDetail).Include(x => x.Comments).FirstOrDefaultAsync();
            if (product is not null)
            {
                return Results.Ok(product);
            }
            throw new ProductNotFoundException();

        }
    }
}
