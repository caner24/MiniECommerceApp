using MediatR;
using MiniECommerceApp.Application.MiniECommerce.Queries.Response;
using MiniECommerceApp.Entity.Helpers;
using MiniECommerceApp.Entity.Models;
using MiniECommerceApp.Entity.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Queries.Request
{
    public class GetAllProductQueryRequest : ProductParameters, IRequest<PagedList<MiniECommerceApp.Entity.Models.Entity>>
    {

    }
}
