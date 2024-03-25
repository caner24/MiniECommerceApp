namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class InvoicesApiEndpointRouteBuilderExtensions
    {
        public static void MapInvoicesApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/addInvoices/", AddInvoices);
        }

        private static IResult AddInvoices()
        {
            return Results.Ok();
        }

    }
}
