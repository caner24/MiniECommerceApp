using MediatR;
using MiniECommerceApp.Application.MiniECommerce.Commands.Response;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Commands.Request
{
    public record AddComentsToProductCommandRequest : AddCommentsToProductDto, IRequest<AddCommentsToProductResponse>
    {

    }
}
