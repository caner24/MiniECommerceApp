using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Category : IEntity
    {
        public Category()
        {
            Products = new List<Product>();
        }
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public List<Product> Products { get; set; }
    }
}
