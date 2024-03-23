using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Data.Abstract;
using Newtonsoft.Json;
using System.IO;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class FileApiEndpointRouteBuilderExtensions
    {
        public static void MapFileApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/addProductPhotos", AddProductPhotos);
            endpoints.MapGet("/getProductPhotos/{id}", GetProductPhotos);
        }
        private async static Task<IResult> AddProductPhotos(IProductDal productDal, IFormFileCollection formFileCollection, int productId)
        {
            var product = await productDal.Get(x => x.Id == productId).FirstOrDefaultAsync();
            if (product is not null)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);
                foreach (var item in formFileCollection)
                {
                    var path = Path.Combine(folder, item?.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                        product.ProductPhotos.Add(new Entity.Photos { PhotosUrl = path });
                    }
                }
                var response = await productDal.UpdateAsync(product);
                return Results.Ok(response);
            }
            return Results.BadRequest();

        }
        private async static Task<IResult> GetProductPhotos(IProductDal productDal, [FromRoute] int id)
        {
            var product = await productDal.Get(x => x.Id == id).Include(x => x.ProductPhotos).FirstOrDefaultAsync();
            if (product is not null)
            {
                var photoList = product.ProductPhotos.Select(x => x.PhotosUrl);

                JsonSerializerSettings jsonSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };

                return Results.Ok(JsonConvert.SerializeObject(photoList, jsonSettings));

            }
            return Results.BadRequest();

        }
    }
}
