using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Queries.Request;
using MiniECommerceApp.Application.MiniECommerce.Queries.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.QueryHandler
{
    public class GetUserBasketQueryHandler : IRequestHandler<GetUserBasketQueryRequest, GetUserBasketQueryResponse>
    {
        private readonly ClaimsPrincipal _claimPrincipal;
        private readonly IBasketDal _basketDal;
        public GetUserBasketQueryHandler(IBasketDal basketDal, ClaimsPrincipal claimPrincipal)
        {
            _basketDal = basketDal;
            _claimPrincipal = claimPrincipal;
        }
        public async Task<GetUserBasketQueryResponse> Handle(GetUserBasketQueryRequest request, CancellationToken cancellationToken)
        {
            var userName= _claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var userBasket = await _basketDal.Get(x=>x.UserId==userName).FirstOrDefaultAsync();
            if (userBasket == null)
            {
                throw new EmptyBasketException();
            }
            return new GetUserBasketQueryResponse { Products = userBasket.Products };
        }
    }
}
