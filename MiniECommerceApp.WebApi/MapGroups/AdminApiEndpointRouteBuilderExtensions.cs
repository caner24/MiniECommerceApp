using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Data.Abstract;
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
            endpoints.MapPut("/getAllCategories", GetAllCategories);
            endpoints.MapPost("/addCategory", AddCategory);
            endpoints.MapDelete("/deleteCategory/{id}", DeleteCategory);
            endpoints.MapPut("/updateCategory", UpdateCategory);
        }
        private static IResult Get()
        {
            return Results.Content($"<h2>Hi !. Welcome to the admin page </h2>", "text/html");
        }

        private async static Task<IResult> GetAllCategories([FromServices] ICategoryDal _categoryDal)
        {
            var categories = await _categoryDal.GetAll().ToListAsync();
            return Results.Ok(categories);
        }

        private static async Task<IResult> AddProduct([FromServices] IValidator<AddProductDto> validator, [FromServices] IMediator mediator, [FromBody] AddProductCommandRequest addProductCommandRequest)
        {
            ValidationResult validationResult = await validator.ValidateAsync(addProductCommandRequest);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());

            var addedProduct = await mediator.Send(addProductCommandRequest);
            if (addedProduct.IsAdded)
                return Results.Ok(addedProduct);

            return Results.BadRequest(addedProduct.IsAdded);
        }
        private static async Task<IResult> DeleteProduct([FromServices] IMediator mediator, [FromRoute] int id)
        {
            var deletedProduct = await mediator.Send(new DeleteProductCommandRequest { Id = id });
            return Results.Ok(deletedProduct);
        }
        private static async Task<IResult> UpdateProduct([FromServices] IValidator<UpdateProductDto> validator, [FromServices] IMediator mediator, [FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            ValidationResult validationResult = await validator.ValidateAsync(updateProductCommandRequest);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());
            var updatedProduct = await mediator.Send(updateProductCommandRequest);
            return Results.Ok(updatedProduct);
        }

        private static async Task<IResult> AddCategory([FromServices] IValidator<AddCategoryDto> validator, [FromServices] IMediator mediator, AddCategoryCommandRequest addCategoryCommandRequest)
        {
            ValidationResult validationResult = await validator.ValidateAsync(addCategoryCommandRequest);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());
            var addedCategory = await mediator.Send(addCategoryCommandRequest);
            if (addedCategory.IsAdded)
                return Results.Ok(addedCategory);

            return Results.BadRequest(addedCategory.IsAdded);
        }
        private static async Task<IResult> DeleteCategory([FromServices] IValidator<DeleteCategoryDto> validator, IMediator mediator, [FromRoute] int id)
        {
            var addedCategory = await mediator.Send(new DeleteCategoryCommandRequest { Id = id });
            return Results.Ok(addedCategory);
        }
        private static async Task<IResult> UpdateCategory([FromServices] IValidator<UpdateCategoryDto> validator, [FromServices] IMediator mediator, [FromBody] UpdateCategoryCommandRequest updateCategoryCommandRequest)
        {
            ValidationResult validationResult = await validator.ValidateAsync(updateCategoryCommandRequest);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());
            var updatedCategory = await mediator.Send(updateCategoryCommandRequest);
            return Results.Ok(updatedCategory);

        }
    }
}
