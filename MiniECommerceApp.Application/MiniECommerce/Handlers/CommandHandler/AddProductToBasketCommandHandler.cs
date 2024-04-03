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
            var basket = await _basketDal.GetAll().Include(x => x.User).Where(x => x.User.UserName == request.UserId).FirstOrDefaultAsync();
            if (basket.Products is null)
            {
                var newBasket = new Basket { UserId = request.UserId, Products = new List<Product>() };
                foreach (var item in request.ProdId)
                {
                    var products = await _productDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                    if (products is not null)
                        newBasket.Products.Add(products);
                }
                var addedBasket = await _basketDal.AddAsync(newBasket);
                return new AddProductToBasketCommandResponse { Basket = newBasket, IsBaketAdded = true };
            }
            else
            {
                foreach (var item in request.ProdId)
                {
                    var products = await _productDal.Get(x => x.Id == item).FirstOrDefaultAsync();
                    if (products is not null)
                        basket.Products.Add(products);
                }
                var addedBasket = await _basketDal.UpdateAsync(basket);
            }

            return new AddProductToBasketCommandResponse { Basket = basket, IsBaketAdded = true };
        }
    }
}
