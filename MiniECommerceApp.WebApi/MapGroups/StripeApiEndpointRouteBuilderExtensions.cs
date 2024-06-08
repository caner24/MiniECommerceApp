using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniECommerceApp.WebApi.Configuration;
using Stripe;
using Stripe.Checkout;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class StripeApiEndpointRouteBuilderExtensions
    {
        public static void MapStripeApi(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("/create-checkout-session", async (HttpContext context,IConfiguration configuration) =>
            {

                var domain = "https://www.caneraycelep.social";
                var options = new SessionCreateOptions
                {
                    LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1PPTq4FLUeB1O3RtnkGei6vE",
                    Quantity = 1,
                  },
                },
                    Mode = "payment",
                    SuccessUrl = domain + "/success",
                    CancelUrl = domain + "/cancel",
                };
                var service = new SessionService();
                Session session = service.Create(options);

                context.Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            });
        }

    }
}
