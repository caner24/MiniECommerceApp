using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Data.Abstract;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.StaticFiles;
using MiniECommerceApp.Data.Concrete;
using System.IO.Compression;

namespace MiniECommerceApp.WebApi.MapGroups
{
    public static class FileApiEndpointRouteBuilderExtensions
    {
        public static void MapFileApi(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/getProductPhotos/{id}", GetProductPhotos);
            endpoints.MapPost("/addProductPhotos", AddProductPhotos).RequireAuthorization(x =>
            {
                x.RequireRole("Admin");
            });
            endpoints.MapGet("/downloadProductPhotos/{id}", Download).RequireAuthorization(x =>
            {
                x.RequireRole("Admin");
            });
        }
        private async static Task<IResult> AddProductPhotos(IProductDal productDal, IFormFileCollection formFileCollection, int productId)
        {
            var product = await productDal.Get(x => x.Id == productId).Include(x => x.ProductDetail).FirstOrDefaultAsync();
            if (product is not null)
            {
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "Media");
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                product.ProductPhotos.Clear();
                foreach (var item in formFileCollection)
                {
                    var path = Path.Combine(folder, item?.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                        product.ProductPhotos.Add(new Entity.Photos { PhotosUrl = path.Replace("/app/Media", "Files") });
                    }
                }
                var response = await productDal.UpdateAsync(product);
                return Results.Ok(response);
            }
            return Results.BadRequest();
        }
        private async static Task<IResult> GetProductPhotos(IProductDal productDal, [FromRoute] int id)
        {
            var product = await productDal.Get(x => x.Id == id).Include(x => x.ProductPhotos).AsNoTracking().FirstOrDefaultAsync();
            if (product is not null)
            {
                if (product.ProductPhotos.Count > 1)
                {
                    var photoList = product.ProductPhotos.Select(x => new
                    {
                        PhotoUrl = x.PhotosUrl
                    }).ToList();

                    return Results.Ok(photoList);
                }
                var photosUrl = product.ProductPhotos.Take(1).FirstOrDefault().PhotosUrl.Replace("Files", "Media");
                var provider = new FileExtensionContentTypeProvider();

                if (!provider.TryGetContentType(photosUrl, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(photosUrl);
                return Results.File(bytes, contentType, Path.GetFileName(photosUrl));
            }
            return Results.BadRequest();
        }

        public static async Task<IResult> Download(IProductDal productDal, [FromRoute] int id)
        {
            var product = await productDal.Get(x => x.Id == id).Include(x => x.ProductPhotos).AsNoTracking().FirstOrDefaultAsync();
            var provider = new FileExtensionContentTypeProvider();

            if (product is not null)
            {
                var photoUrls = product.ProductPhotos.Select(x => x.PhotosUrl.Replace("Files","Media")).ToList();
                var zipFileName = $"product_photos_{Guid.NewGuid()}.zip";
                var zipFilePath = Path.Combine(Path.GetTempPath(), zipFileName);
                if (System.IO.File.Exists(zipFilePath))
                {
                    System.IO.File.Delete(zipFilePath);
                }
                using (var zipArchive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    foreach (var photoUrl in photoUrls)
                    {
                        var photoBytes = await System.IO.File.ReadAllBytesAsync(photoUrl);
                        var entry = zipArchive.CreateEntry(Path.GetFileName(photoUrl));
                        using (var entryStream = entry.Open())
                        {
                            await entryStream.WriteAsync(photoBytes, 0, photoBytes.Length);
                        }
                    }
                }
                if (!provider.TryGetContentType(zipFilePath, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var zipFileBytes = await System.IO.File.ReadAllBytesAsync(zipFilePath);
                return Results.File(zipFileBytes, contentType, zipFileName);
            }

            return Results.NotFound();
        }
    }
}
