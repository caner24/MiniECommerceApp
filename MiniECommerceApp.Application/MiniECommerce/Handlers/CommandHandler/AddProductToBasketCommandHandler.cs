using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommandRequest, AddProductToBasketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketDal _basketDal;
        public AddProductToBasketCommandHandler(IBasketDal basketDal, IMapper mapper)
        {
            _mapper = mapper;
            _basketDal = basketDal;
        }
        public async Task<AddProductToBasketCommandResponse> Handle(AddProductToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketDal.Get(x => x.UserId == request.UserId).Include(x => x.Products).FirstOrDefaultAsync();
            if (basket is null)
            {
                var newBasket = new Basket { UserId = request.UserId };
                foreach (var item in request.Products)
                {
                    newBasket.Products.Add(item);
                }
                var addedBasket = await _basketDal.AddAsync(newBasket);
                return new AddProductToBasketCommandResponse { Basket = newBasket, IsBaketAdded = true };
            }
            else
            {
                foreach (var item in request.Products)
                {
                    basket.Products.Add(item);
                }
                var addedBasket = await _basketDal.UpdateAsync(new Basket { UserId = request.UserId });
            }

            return new AddProductToBasketCommandResponse { Basket = basket, IsBaketAdded = true };
        }
    }
}
