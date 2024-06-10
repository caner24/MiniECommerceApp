using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniECommerceApp.Core.CrosssCuttingConcerns.MailService;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.WebApi.Configuration;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;
using System.Xml.Linq;
using IEmailSender = MiniECommerceApp.Data.Abstract.IEmailSender;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class StripeApiEndpointRouteBuilderExtensions
    {
        public static void MapStripeApi(this IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapPost("/create-stripe-user", CreateStripeUser);
            endpointRouteBuilder.MapPost("/stripe-web-hook", CreateStripeWebHook);
            endpointRouteBuilder.MapPost("/create-checkout-session", CreateCheckoutSession).RequireAuthorization();
        }


        public static async Task<IResult> CreateStripeWebHook(HttpContext context, [FromServices] IEmailSender emailSender)
        {
            var json = await new StreamReader(context.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                    json,
                    context.Request.Headers["Stripe-Signature"],
                    "whsec_AxtjthM3CIjxuKMUvCAjGBFBFiNq97Is"
                );

                // Ödeme başarılı olduğunda yapılacak işlemler
                if (stripeEvent.Type == Events.CheckoutSessionCompleted)
                {
                    var session = stripeEvent.Data.Object as Session;
                    var customerEmail = session?.CustomerEmail;

                    if (!string.IsNullOrEmpty(customerEmail))
                    {
                        var messageSender = new Message(new string[] { customerEmail }, "Receipt", "<h1>Your payment was successful!</h1>", null);
                        await emailSender.SendEmailAsync(messageSender);
                    }
                    else
                    {
                        // Email adresi yok veya boş
                        throw new InvalidOperationException("Customer email is null or empty.");
                    }
                }

                return Results.Ok(); // HTTP 200 yanıtı
            }
            catch (StripeException e)
            {
                // Stripe özel hataları
                return Results.BadRequest(new { error = "StripeException", message = e.Message });
            }
            catch (Exception e)
            {
                // Genel hatalar
                return Results.BadRequest(new { error = "GeneralException", message = e.Message });
            }
        }


        public class CheckoutSessionRequest
        {
            public Dictionary<string, int> ProductPriceAndAmount { get; set; }
        }
        private static async Task<StatusCodeResult> CreateCheckoutSession(ClaimsPrincipal claims, [FromBody] Dictionary<string, int> productPriceAndAmount, HttpContext context, [FromServices] MiniECommerceContext dbContext, [FromServices] IConfiguration configuration)
        {
            var userId = claims.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var domain = "https://www.caneraycelep.social";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                },
                Mode = "payment",
                SuccessUrl = domain + "/success",
                CancelUrl = domain + "/cancel",
                Metadata = new Dictionary<string, string>
        {
            { "user_id", user.StripeUserId }
        },
                CustomerEmail = user.Email,
            };
            foreach (var item in productPriceAndAmount)
            {
                options.LineItems.Add(new SessionLineItemOptions
                {
                    Price = item.Key,
                    Quantity = item.Value,
                });
            }

            var service = new SessionService();
            Session session = service.Create(options);

            context.Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        private static async Task<IResult> CreateStripeUser([FromServices] StripeClient stripeClient, [FromServices] MiniECommerceContext context, string email)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user is not null)
            {
                var options = new CustomerCreateOptions
                {
                    Email = email,
                    Name = user.UserName,
                };
                var service = new CustomerService(stripeClient);
                var customer = await service.CreateAsync(options);
                user.StripeUserId = customer.Id;
                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Results.Ok("Stripe user added");
            }
            else
            {
                return Results.NotFound("User not found.");
            }
        }

    }
}
