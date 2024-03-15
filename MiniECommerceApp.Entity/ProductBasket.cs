using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class ProductBasket
    {

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }

    }
}
