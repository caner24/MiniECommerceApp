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
            return Results.Ok();
        }


        private static IResult GetAllInvoices()
        {
            return Results.Ok();
        }
    }
}
