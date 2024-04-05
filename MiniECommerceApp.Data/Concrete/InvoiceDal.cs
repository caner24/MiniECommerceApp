using MiniECommerceApp.Core.DataAccess;
using MiniECommerceApp.Core.DataAccess.EntityFramework;
using MiniECommerceApp.Data.Abstract;
using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Concrete
{
    public class InvoiceDal : EFRepositoryBase<MiniECommerceContext, Invoice>, IInvoicesDal
    {
        public InvoiceDal(MiniECommerceContext tContext) : base(tContext)
        {
        }
    }
}
