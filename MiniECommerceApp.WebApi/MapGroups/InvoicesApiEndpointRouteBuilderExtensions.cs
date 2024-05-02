using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing;
using System.Threading;
using System.Xml.Linq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using FluentValidation;
using MiniECommerceApp.Entity.DTOs;
using FluentValidation.Results;
using MiniECommerceApp.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class InvoicesApiEndpointRouteBuilderExtensions
    {
        public static void MapInvoicesApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/createInvoices", CreateInvoices);
            endpoints.MapGet("/getInvoices/{id}", GetInvoices);
        }

        private static async Task<IResult> CreateInvoices([FromServices] IValidator<CreateInvoiceDto> validator, [FromServices] IMediator mediator, [FromBody] CreateInvoiceCommandRequest createInvoicesCommandRequest)
        {
            ValidationResult validationResult = await validator.ValidateAsync(createInvoicesCommandRequest);
            if (!validationResult.IsValid)
                return Results.ValidationProblem(validationResult.ToDictionary());
            var response = await mediator.Send(createInvoicesCommandRequest);
            return Results.Ok(response);
        }

        private static async Task<IResult> GetInvoices([FromRoute] string id, [FromServices] IInvoicesDal invoicesDal)
        {
            var invoices = invoicesDal.GetAll(x => x.UserId == id).ToListAsync();
            return Results.Ok(invoices);
        }

    }
}
