using MediatR;
using MiniECommerceApp.Application.MiniECommerce.Queries.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Queries.Request
{
    public record GetUserBasketQueryRequest:IRequest<GetUserBasketQueryResponse>
    {
        public string UserId { get; set; }
    }
}
