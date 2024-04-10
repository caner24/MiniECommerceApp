using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class CommentsApiEndpointRouteBuilderExtensions
    {
        public static void MapCommentApi(this IEndpointRouteBuilder routeBuilder)
        {
            routeBuilder.MapPost("addCommentToProduct", AddCommentToProduct);
        }

        public static async Task<IResult> AddCommentToProduct([FromServices] IMediator mediator, [FromBody] AddComentsToProductCommandRequest addComentsToProductCommandRequest)
        {
            var response = await mediator.Send(addComentsToProductCommandRequest);
            return Results.Ok(response);
        }
    }
}
