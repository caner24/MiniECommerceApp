using AutoMapper;
using MediatR;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
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
        private readonly IProductDal _productDal;
        private readonly IMapper _mapper;
        public AddProductCommandHandler(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }
        public async Task<AddProductCommandResponse> Handle(AddProductCommandRequest request, CancellationToken cancellationToken)
        {

            var product = _mapper.Map<Product>(request);
            var addedProduct = await _productDal.AddAsync(product);

            return new AddProductCommandResponse { IsAdded = true, Product = addedProduct };
        }
    }
}
