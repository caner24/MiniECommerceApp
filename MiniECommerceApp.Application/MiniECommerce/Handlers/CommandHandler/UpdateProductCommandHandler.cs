using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var isProductIsNotNull = await _productDal.Get(x => x.Id == request.Id).Include(x => x.ProductDetail).Include(x => x.Categories).FirstOrDefaultAsync();
            if (isProductIsNotNull is not null)
            {
                var product = _mapper.Map<Product>(request);
                var updatedProduct = await _productDal.UpdateAsync(product);
                return new UpdateProductCommandResponse { IsUpdated = true, Product = product };
            }
            throw new ProductNotFoundException();
        }
    }
}
