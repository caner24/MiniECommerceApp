using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using MiniECommerceApp.Entity;
using MiniECommerceApp.WebApi.Extensions;
using MiniECommerceApp.WebApi.MapGroups;
using System.Reflection;
using System.Text.Json.Serialization;
using MiniECommerceApp.WebApi.SeedData;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Text.Json;
using Prometheus;
using MiniECommerceApp.WebApi.TaskScheduler;
using Stripe;
using System.Security.Claims;
var builder = WebApplication.CreateBuilder(args);


StripeConfiguration.ApiKey = "rk_test_51NjQanFLUeB1O3Rt04rBvF2x6t7juO9NX3U7cy1g6cTRXcTPxqhj9wI3P8moHTSXQXnABu9Bc8K2L67vLlbYjzrX00q3amNz8U";
builder.Configuration.AddUserSecrets<Program>().Build();
builder.Services.IdentityConfiguration(builder.Configuration);
builder.Services.SwaggerConfiguration();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.ServiceLifetimeSetup(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.Load("MiniECommerceApp.Application")));
builder.Services.ConfigureCors();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAntiforgery();
builder.Services.RedisCacheSettings(builder.Configuration,builder.Environment);
builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<ClaimsPrincipal>(provider =>
{
    var context = provider.GetService<IHttpContextAccessor>();
    return context?.HttpContext?.User ?? new ClaimsPrincipal();
});
//builder.Services.AddRateLimiting();
builder.Services.AddHostedService<InitializationBackgroundService>();
builder.Services.FluentValidationRegister();
builder.Services.StripeOptions(builder.Configuration);

JsonSerializerSettings serializerSettings = new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});


await app.IsAdminUserExist();
app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Media")),
    RequestPath = "/Files"
});

#region Apis

app.UseAntiforgery();
app.MapGroup("/api/identity").MapIdentityApi<User>();
app.MapGroup("/api/admin").RequireAuthorization(x =>
{
    x.RequireAuthenticatedUser();
    x.RequireRole("Admin");
}).MapAdminApi();
app.MapGroup("/api/basket").RequireAuthorization(x => { x.RequireAuthenticatedUser(); }).MapBasketApi();
app.MapGroup("/api/product").MapProductApi();
app.MapGroup("/api/file").DisableAntiforgery().MapFileApi();
app.MapGroup("/api/invoices").RequireAuthorization(x => { x.RequireAuthenticatedUser(); }).MapInvoicesApi();
app.MapGroup("api/comments").RequireAuthorization(x => { x.RequireAuthenticatedUser(); }).MapCommentApi();
app.MapGroup("api/stripe").MapStripeApi();

#endregion
app.UseHttpMetrics();
app.MapMetrics();
//app.UseRateLimiter();
app.UseExceptionHandler();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.Run();