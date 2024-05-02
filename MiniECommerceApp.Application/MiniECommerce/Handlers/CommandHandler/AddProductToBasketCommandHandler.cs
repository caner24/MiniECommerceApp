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
    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommandRequest, AddProductToBasketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketDal _basketDal;
        private readonly IProductDal _productDal;
        public AddProductToBasketCommandHandler(IProductDal productDal, IBasketDal basketDal, IMapper mapper)
        {
            _mapper = mapper;
            _basketDal = basketDal;
            _productDal = productDal;
        }
        public async Task<AddProductToBasketCommandResponse> Handle(AddProductToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var basket = await _basketDal.GetAll().Include(x => x.User).Where(x => x.User.Email == request.UserId).FirstOrDefaultAsync();
            if (basket.Products.Count == 0)
            {
                var products = await _productDal.Get(x => x.Id == request.ProdId).AsNoTracking().FirstOrDefaultAsync();
                if (products is not null)
                {
                    products.Amount = request.Amount;
                    basket.Products.Add(products);
                }
                if (basket.UserId != null)
                {
                    await _basketDal.UpdateAsync(basket);
                    return new AddProductToBasketCommandResponse { Basket = basket, IsBaketAdded = true };
                }

                var addedBaskets = await _basketDal.AddAsync(basket);
                return new AddProductToBasketCommandResponse { Basket = basket, IsBaketAdded = true };
            }
            else
            {

                var products = await _productDal.Get(x => x.Id == request.ProdId).FirstOrDefaultAsync();
                if (products is not null)
                {

                    if (products.Id == request.ProdId)
                    {
                        basket.Products.Where(x => x.Id == request.ProdId).FirstOrDefault().Amount += request.Amount;
                    }
                    else
                    {
                        basket.Products.Add(products);
                    }
                }

            }
            var addedBasket = await _basketDal.UpdateAsync(basket);

            return new AddProductToBasketCommandResponse { Basket = basket, IsBaketAdded = true };
        }
    }
}
