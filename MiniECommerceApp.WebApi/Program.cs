using Microsoft.OpenApi.Models;
using MiniECommerceApp.Entity;
using MiniECommerceApp.WebApi.DesignTime;
using MiniECommerceApp.WebApi.Extensions;
using MiniECommerceApp.WebApi.MapGroups;

var builder = WebApplication.CreateBuilder(args);

builder.Services.IdentityConfiguration(builder.Configuration);
builder.Services.SwaggerConfiguration();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.ServiceLifetimeSetup();
builder.Services.AddProblemDetails();builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureCors();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.Services.MigrateDb();
app.UseHttpsRedirection();
#region Apis
app.MapGroup("/api/identity").MapIdentityApi<User>();
app.MapGroup("/api/product").MapProductApi();
app.MapGroup("/api/admin").RequireAuthorization(x =>
{
    x.RequireAuthenticatedUser();
    x.RequireRole("Admin");
}).MapAdminApi();
app.MapGroup("/api/basket").RequireAuthorization(x => { x.RequireAuthenticatedUser(); }).MapBasketApi();

#endregion
app.UseExceptionHandler();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.Run();