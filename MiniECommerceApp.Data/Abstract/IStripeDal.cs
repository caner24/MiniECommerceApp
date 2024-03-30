using MiniECommerceApp.Entity.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Abstract
{
    public interface IStripeDal
    {
        Task<CustomerResource> CreateCustomer(CreateCustomerResource resource, CancellationToken cancellationToken);
        Task<ChargeResource> CreateCharge(CreateChargeResource resource, CancellationToken cancellationToken);

    }
}
