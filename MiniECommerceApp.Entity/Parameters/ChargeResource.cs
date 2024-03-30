using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Parameters
{
    public record ChargeResource(
     string ChargeId,
     string Currency,
     long Amount,
     string CustomerId,
     string ReceiptEmail,
     string Description);
}
