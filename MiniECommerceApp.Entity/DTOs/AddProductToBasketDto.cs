using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record AddProductToBasketDto
    {
        public string UserName { get; init; }

        public HashSet<Product> Products { get; init; }

    }
}
