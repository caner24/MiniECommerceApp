using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Parameters
{
    public record CreateCustomerResource(
       string Email,
       string Name,
       CreateCardResource Card);
}
