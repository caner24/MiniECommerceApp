using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Queries.Request;
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
            endpoints.MapPost("/updateUserBasket", UpdateUserBasket);
            endpoints.MapGet("/getUserBasket/{id}", GetUserBasket);
        }
        private static async Task<IResult> AddProductToBasket([FromServices] IValidator<AddProductToBasketCommandRequest> validator, [FromServices] IMediator mediator, [FromBody] AddProductToBasketCommandRequest addProductToBasketDto)
        {
            ValidationResult validationResult = await validator.ValidateAsync(addProductToBasketDto);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());
            var response = await mediator.Send(addProductToBasketDto);
            return Results.Ok(response);
        }
        private static async Task<IResult> GetUserBasket([FromServices] IMediator mediator, [FromRoute] string id)
        {
            var response = await mediator.Send(new GetUserBasketQueryRequest { UserId = id });
            return Results.Ok(response);
        }
        private static async Task<IResult> UpdateUserBasket([FromServices] IMediator mediator, UpdateProductToBasketRequest updateProductToBasketDto)
        {
            var response = await mediator.Send(updateProductToBasketDto);
            return Results.Ok(response);
        }

    }
}
