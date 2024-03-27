using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Data.Concrete;
using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommandRequest, AddProductCommandResponse>
    {
        private readonly ICategoryDal _categoryDal;
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(IProductDal productDal, ICategoryDal categoryDal, IMapper mapper)
        {
            _categoryDal = categoryDal;
            _productDal = productDal;
            _mapper = mapper;
        }
        public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _mapper.Map<Product>(request);
                foreach (var item in request.CategoriesId)
                {
                    var isCategoryExist = await _categoryDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                    if (isCategoryExist is not null)
                    {
                        product.Categories.Add(isCategoryExist);
                    }
                }
                var addedProduct = await _productDal.AddAsync(product);
                if (addedProduct is not null)
                {
                    return new AddProductCommandResponse { IsAdded = true, Product = addedProduct };
                }
                return new AddProductCommandResponse { IsAdded = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                throw;
            }

        }
    }
}
