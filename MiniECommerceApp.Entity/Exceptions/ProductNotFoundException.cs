using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Exceptions
{
    public class ProductNotFoundException : BaseException
    {
        public ProductNotFoundException() : base($"Your product with id did not find")
        {
        }
    }
}
