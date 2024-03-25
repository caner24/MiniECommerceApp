using MiniECommerceApp.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.Helpers
{
    public interface IDataShaper<T>
    {
        IEnumerable<MiniECommerceApp.Entity.Models.Entity> ShapeData(IEnumerable<T> entities, string fieldsString);
        MiniECommerceApp.Entity.Models.Entity ShapeData(T entity, string fieldsString);
    }
}
