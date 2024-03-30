using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Parameters
{
    public record CreateCardResource(
        string Name,
        string Number,
        string ExpiryYear,
        string ExpiryMonth,
        string Cvc);
    {
    }
}
