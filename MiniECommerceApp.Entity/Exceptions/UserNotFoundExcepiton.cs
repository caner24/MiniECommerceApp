using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Exceptions
{
    public class UserNotFoundExcepiton : BaseException
    {
        public UserNotFoundExcepiton() : base($"Your user with name  did not find")
        {
        }
    }
}
