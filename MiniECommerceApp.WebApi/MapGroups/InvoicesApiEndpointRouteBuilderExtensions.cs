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
using System.Security.Claims;
using MiniECommerceApp.Data.Concrete;
using Stripe;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class InvoicesApiEndpointRouteBuilderExtensions
    {
        public static void MapInvoicesApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/getInvoices/", GetInvoices);
        }

        private static async Task<IResult> GetInvoices(ClaimsPrincipal claimPrincipal,[FromServices]MiniECommerceContext context,  [FromServices] IInvoicesDal invoicesDal)
        {
            var userId = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var user=context.Users.FirstOrDefault(x => x.Id == userId);
            var options = new ChargeListOptions
            {
                Customer = user.StripeUserId,
                Limit = 100 // İhtiyacınıza göre limit ayarlayın
            };

            var service = new ChargeService();
            var charges = await service.ListAsync(options);
            return Results.Ok(charges.Data);
        }

    }
}
