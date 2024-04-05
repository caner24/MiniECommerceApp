namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class InvoicesApiEndpointRouteBuilderExtensions
    {
        public static void MapInvoicesApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/addInvoices/", CreateInvoices);
        }

        private static IResult CreateInvoices()
        {
            return Results.Content("", "text/html");
        }


        private static IResult GetInvoices()
        {
            return Results.Ok();
        }
    }
}
