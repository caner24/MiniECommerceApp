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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Handlers.CommandHandler
{
    public class AddProductToBasketCommandHandler : IRequestHandler<AddProductToBasketCommandRequest, AddProductToBasketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBasketDal _basketDal;
        private readonly IProductDal _productDal;
        private readonly MiniECommerceContext _context;
        private readonly ClaimsPrincipal _claimPrincipal;
        public AddProductToBasketCommandHandler(MiniECommerceContext context, IProductDal productDal, IBasketDal basketDal, IMapper mapper, ClaimsPrincipal claimPrincipal)
        {
            _mapper = mapper;
            _basketDal = basketDal;
            _productDal = productDal;
            _claimPrincipal = claimPrincipal;
            _context = context;
        }
        public async Task<AddProductToBasketCommandResponse> Handle(AddProductToBasketCommandRequest request, CancellationToken cancellationToken)
        {
            var userEmail = _claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            var basket = await _basketDal.GetAll().Include(x => x.User).Where(x => x.User.Email == userEmail).FirstOrDefaultAsync();
            if (basket is null)
            {
                basket = new Basket();

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
                else
                {
                    var user = await _context.Users.Where(x => x.Email == userEmail).FirstOrDefaultAsync();
                    basket.User = user;
                }

                var addedBaskets = await _basketDal.AddAsync(basket);
                return new AddProductToBasketCommandResponse { Basket = basket, IsBaketAdded = true };
            }
            else
            {

                var products = await _productDal.Get(x => x.Id == request.ProdId).FirstOrDefaultAsync();
                if (products is not null)
                {
                    var prod = basket.Products.Select(x => x.Id).Where(x => x == request.ProdId).FirstOrDefault();
                    if (prod != 0)
                    {
                        basket.Products.Where(x => x.Id == request.ProdId).FirstOrDefault().Amount += request.Amount;
                    }
                    else
                    {
                        products.Amount = request.Amount;
                        basket.Products.Add(products);
                    }
                }

            }
            var addedBasket = await _basketDal.UpdateAsync(basket);

            return new AddProductToBasketCommandResponse { Basket = basket, IsBaketAdded = true };
        }
    }
}
