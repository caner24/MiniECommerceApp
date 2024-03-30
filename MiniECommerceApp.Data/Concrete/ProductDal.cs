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
    public class ProductDal : EFRepositoryBase<MiniECommerceContext, Product>, IProductDal
    {
        public ProductDal(MiniECommerceContext tContext) : base(tContext)
        {

        }
    }
}
