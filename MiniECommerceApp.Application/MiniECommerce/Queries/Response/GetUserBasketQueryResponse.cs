using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Queries.Response
{
    public class GetUserBasketQueryResponse
    {
        public List<Product> Products { get; set; }

    }
}
