using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Queries.Request;
using MiniECommerceApp.Application.MiniECommerce.Queries.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.QueryHandler
{
    public class GetUserBasketQueryHandler : IRequestHandler<GetUserBasketQueryRequest, GetUserBasketQueryResponse>
    {
        private readonly IBasketDal _basketDal;
        public GetUserBasketQueryHandler(IBasketDal basketDal)
        {
            _basketDal = basketDal;
        }
        public async Task<GetUserBasketQueryResponse> Handle(GetUserBasketQueryRequest request, CancellationToken cancellationToken)
        {
            var userBasket = await _basketDal.GetAll(x => x.UserId == request.UserId).FirstOrDefaultAsync();
            if (userBasket == null)
            {
                throw new EmptyBasketException();
            }
            return new GetUserBasketQueryResponse { Products = userBasket.Products };
        }
    }
}
