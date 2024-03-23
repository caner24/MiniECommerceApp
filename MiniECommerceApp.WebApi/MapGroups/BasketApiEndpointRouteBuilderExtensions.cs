using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.DTOs;
using MiniECommerceApp.Entity.Exceptions;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class BasketApiEndpointRouteBuilderExtensions
    {
        public static void MapBasketApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/addProductToBasket", AddProductToBasket);
            endpoints.MapGet("/getUserBasket/{UserName}", GetUserBasket);
        }
        private static async Task<IResult> AddProductToBasket(IServiceProvider services, [FromBody] AddProductToBasketDto addProductToBasketDto)
        {
            await FindUser(services, addProductToBasketDto.UserName);
            return Results.Ok();
        }
        private static async Task<IResult> GetUserBasket(IServiceProvider services, [FromRoute] string userName)
        {
            await FindUser(services, userName);
            return Results.Ok();
        }
        private static async Task FindUser(IServiceProvider services, string userName)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var user = await userManager.FindByNameAsync(userName);
            if (user is null)
            {
                throw new UserNotFoundExcepiton();
            }
        }

    }
}
