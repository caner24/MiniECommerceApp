using MediatR;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Commands.Request
{
    public record UpdateProductToBasketRequest : IRequest<UpdateProductToBasketResponse>
    {
        public List<int> ProdId { get; init; }
    }
}
