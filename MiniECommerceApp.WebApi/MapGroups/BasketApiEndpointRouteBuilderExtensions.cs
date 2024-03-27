﻿using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            endpoints.MapGet("/getUserBasket/{id}", GetUserBasket);
        }
        private static async Task<IResult> AddProductToBasket(IMediator mediator, [FromBody] AddProductToBasketDto addProductToBasketDto)
        {
            var response = await mediator.Send(addProductToBasketDto);
            return Results.Ok(response);
        }
        private static async Task<IResult> GetUserBasket(IMediator mediator, [FromRoute] string id)
        {
            var response = await mediator.Send(new GetUserBasketQueryRequest { UserId = id });
            return Results.Ok(response);
        }

    }
}
