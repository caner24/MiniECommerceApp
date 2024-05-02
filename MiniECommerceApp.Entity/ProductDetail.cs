using MiniECommerceApp.Core.DataAccess;
using MiniECommerceApp.Entity.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class ProductDetail : IEntity
    {
        public Size Size { get; set; }
        public int ProductId { get; set; }
        public string ProductDetailText { get; set; }
        public Product Product { get; set; }
    }
}
