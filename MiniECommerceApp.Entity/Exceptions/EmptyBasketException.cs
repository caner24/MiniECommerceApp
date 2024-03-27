using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Exceptions
{
    public class EmptyBasketException : BaseException
    {
        public EmptyBasketException() : base(" Basket you searched did not found !.")
        {
        }
    }
}
