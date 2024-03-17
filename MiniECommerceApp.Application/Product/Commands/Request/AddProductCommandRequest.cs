using MediatR;
using MiniECommerceApp.Application.Product.Commands.Response;
using MiniECommerceApp.Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.Product.Commands.Request
{
    public record AddProductCommandRequest:AddProductDto,IRequest<AddProductCommandResponse>
    {

    }
}
