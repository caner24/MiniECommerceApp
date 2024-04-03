using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Basket : IEntity
    {
        public Basket()
        {
            Products = new List<Product>();
        }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Product> Products { get; set; }
    }
}
