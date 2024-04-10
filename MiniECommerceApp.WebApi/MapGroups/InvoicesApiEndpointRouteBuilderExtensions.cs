using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing;
using System.Threading;
using System.Xml.Linq;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class InvoicesApiEndpointRouteBuilderExtensions
    {
        public static void MapInvoicesApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/CreateInvoices/", CreateInvoices);
        }

        private static IResult CreateInvoices()
        {
         
            return Results.Content("<h1>Ok !.</h1>", "text/html");
        }

    }
}
