using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Exceptions
{
    public class CategoryNotFoundException : BaseException
    {
        public CategoryNotFoundException() : base("Your category with id did not find")
        {
        }
    }
}
