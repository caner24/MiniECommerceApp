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
var builder = WebApplication.CreateBuilder(args);

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
builder.Services.RedisCacheSettings(builder.Configuration);
builder.Services.AddRateLimiting();
builder.Services.AddHostedService<InitializationBackgroundService>();
builder.Services.FluentValidationRegister();


JsonSerializerSettings serializerSettings = new JsonSerializerSettings
{
    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
else
{
    app.UseHsts();
}

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


#endregion
app.UseHttpMetrics();
app.MapMetrics();
app.UseRateLimiter();
app.UseExceptionHandler();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.Run();