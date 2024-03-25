using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        private readonly IProductDal _productDal;
        public DeleteProductCommandHandler(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var isProductNotNull = await _productDal.Get(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (isProductNotNull is not null)
            {
                await _productDal.DeleteAsync(isProductNotNull);
                return new DeleteProductCommandResponse { IsDeleted = true };
            }
            throw new ProductNotFoundException();
        }
    }
}
