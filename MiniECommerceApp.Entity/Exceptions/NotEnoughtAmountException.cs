using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Exceptions
{
    public class NotEnoughtAmountException : BaseException
    {
        public NotEnoughtAmountException() : base("Satın almak istediğiniz miktarda yeterli ürün yoktur !.")
        {
        }
    }
}
