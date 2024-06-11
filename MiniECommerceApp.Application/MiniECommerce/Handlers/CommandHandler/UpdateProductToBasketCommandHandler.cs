using MediatR;
using Microsoft.EntityFrameworkCore;
using MiniECommerceApp.Application.MiniECommerce.Commands.Request;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class UpdateProductToBasketCommandHandler : IRequestHandler<UpdateProductToBasketRequest, UpdateProductToBasketResponse>
    {
        private readonly IBasketDal _basketDal;
        private readonly ClaimsPrincipal _claimsPrincipal;
        public UpdateProductToBasketCommandHandler(IBasketDal basktetDal, ClaimsPrincipal claimsPrincipal)
        {
            _basketDal = basktetDal;
            _claimsPrincipal = claimsPrincipal;
        }
        public async Task<UpdateProductToBasketResponse> Handle(UpdateProductToBasketRequest request, CancellationToken cancellationToken)
        {
            var userEmail = _claimsPrincipal.Claims.First(x => x.Type == ClaimTypes.Email)?.Value;
            var userBasket = await _basketDal.GetAll().Include(x => x.User).Where(x => x.User.Email == userEmail).FirstOrDefaultAsync();
            if (userBasket is not null)
            {
                foreach (var item in request.ProdId)
                {
                    userBasket.Products.RemoveAll(x => x.Id == item);
                }
                var newBasket = await _basketDal.UpdateAsync(userBasket);
                return new UpdateProductToBasketResponse { Basket = newBasket, IsBasketRemoved = true };

            }

            throw new EmptyBasketException();
        }
    }
}
