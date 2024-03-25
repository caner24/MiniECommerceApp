using MiniECommerceApp.Core.DataAccess;
using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Abstract
{
    public interface ICategoryDal : IEFRepositoryBase<Category>
    {
    }
}
