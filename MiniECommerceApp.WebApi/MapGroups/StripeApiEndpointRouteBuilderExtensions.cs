using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MiniECommerceApp.WebApi.Configuration;
using Stripe;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class StripeApiEndpointRouteBuilderExtensions
    {
        public static void MapStripeApi(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapGet("/create-intent", async (IConfiguration configuration) =>
            {

                try
                {
                    long orderAmount = 1400;

                    var service = new PaymentIntentService();
                    PaymentIntent paymentIntent = default;


                    paymentIntent = await service.CreateAsync(new PaymentIntentCreateOptions
                    {
                        Amount = orderAmount,
                        Currency = "USD",
                        AutomaticPaymentMethods = new() { Enabled = true }
                    });


                    return Results.Ok(new { paymentIntent.ClientSecret });
                }
                catch (StripeException e)
                {
                    return Results.BadRequest(new { error = new { message = e.StripeError.Message } });
                }
            });
        }

    }
}
