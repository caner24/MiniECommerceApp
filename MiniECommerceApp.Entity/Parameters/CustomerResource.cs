using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Parameters
{
    public record CustomerResource(
        string CustomerId,
        string Email,
        string Name);
}
