using Microsoft.OpenApi.Models;
using MiniECommerceApp.Entity;
using MiniECommerceApp.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.IdentityConfiguration(builder.Configuration);
builder.Services.SwaggerConfiguration();
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

app.MapGroup("/api/identity").MapIdentityApi<User>();

app.MapGroup("/api/product").MapProductApi();



app.UseAuthentication();
app.UseAuthorization();
app.Run();