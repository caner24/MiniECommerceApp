using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Entity.DTOs;
using Stripe;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class CommentsApiEndpointRouteBuilderExtensions
    {
        public static void MapCommentApi(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("addCommentToProduct", AddCommentToProduct);
        }
        public static async Task<IResult> AddCommentToProduct(IValidator<AddCommentsToProductDto> validator, [FromServices] IMediator mediator, [FromBody] AddComentsToProductCommandRequest addComentsToProductCommandRequest)
        {
            ValidationResult validationResult = await validator.ValidateAsync(addComentsToProductCommandRequest);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var response = await mediator.Send(addComentsToProductCommandRequest);
            return Results.Ok(response);
        }
    }
}
