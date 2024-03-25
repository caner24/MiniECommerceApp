using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Entity.DTOs;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class AdminApiEndpointRouteBuilderExtensions
    {
        public static void MapAdminApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/", Get);

            // Product
            endpoints.MapPost("/addProduct", AddProduct);
            endpoints.MapDelete("/deleteProduct/{id}", DeleteProduct);
            endpoints.MapPut("/updateProduct", UpdateProduct);

            // Category
            endpoints.MapPost("/addCategory", AddCategory);
            endpoints.MapDelete("/deleteCategory/{id}", DeleteCategory);
            endpoints.MapPut("/updateCategory", UpdateCategory);
        }
        private static IResult Get()
        {
            return Results.Content($"<h2>Hi !. Welcome to the admin page </h2>", "text/html");
        }
        private static async Task<IResult> AddProduct(IMediator mediator, AddProductCommandRequest addProductCommandRequest)
        {
            var addedProduct = await mediator.Send(addProductCommandRequest);
            if (addedProduct.IsAdded)
                return Results.Ok(addedProduct);

            return Results.BadRequest(addedProduct.IsAdded);
        }

        private static async Task<IResult> DeleteProduct(IMediator mediator, [FromRoute] int id)
        {
            var deletedProduct = await mediator.Send(new DeleteProductCommandRequest { Id = id });
            return Results.Ok(deletedProduct);
        }

        private static async Task<IResult> UpdateProduct(IMediator mediator, [FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            var updatedProduct = await mediator.Send(updateProductCommandRequest);
            return Results.Ok(updatedProduct);
        }
        private static async Task<IResult> AddCategory(IMediator mediator, AddCategoryCommandRequest addCategoryCommandRequest)
        {
            var addedCategory = await mediator.Send(addCategoryCommandRequest);
            if (addedCategory.IsAdded)
                return Results.Ok(addedCategory);

            return Results.BadRequest(addedCategory.IsAdded);
        }
        private static async Task<IResult> DeleteCategory(IMediator mediator, [FromRoute] int id)
        {
            var addedCategory = await mediator.Send(new DeleteCategoryCommandRequest { Id = id });
            return Results.Ok(addedCategory);
        }
        private static async Task<IResult> UpdateCategory(IMediator mediator, [FromBody] UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            var updatedCategory = await mediator.Send(updateCategoryCommandRequest);
            return Results.Ok(updatedCategory);

        }
    }
}
