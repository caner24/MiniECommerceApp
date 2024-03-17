using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Exceptions
{
    public abstract class BaseException:Exception
    {
        protected BaseException(string ex):base(ex)
        {
            
        }
    }
}
