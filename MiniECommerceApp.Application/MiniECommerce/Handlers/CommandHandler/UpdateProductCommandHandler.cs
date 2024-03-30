using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryDal _categoryDal;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductDal productDal, ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _productDal = productDal;
            _mapper = mapper;
        }
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var isProductIsNotNull = await _productDal.Get(x => x.Id == request.Id).AsNoTracking().FirstOrDefaultAsync();
            if (isProductIsNotNull is not null)
            {
                var product = _mapper.Map<Product>(request);
                if (request.CategoriesId.Count() > 0)
                {
                    foreach (var item in request.CategoriesId)
                    {
                        var isCategoryExist = await _categoryDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                        if (isCategoryExist is not null)
                        {
                            product.Categories.Add(isCategoryExist);
                        }
                    }
                    product.ProductPhotos = isProductIsNotNull.ProductPhotos;
                    product.ConcurrencyToken = isProductIsNotNull.ConcurrencyToken;
                }
                var updatedProduct = await _productDal.UpdateAsync(product);
                return new UpdateProductCommandResponse { IsUpdated = true, Product = product };
            }
            throw new ProductNotFoundException();
        }
    }
}
