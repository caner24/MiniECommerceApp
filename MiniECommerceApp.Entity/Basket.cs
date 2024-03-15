using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Basket
    {
        public Basket()
        {
            Product = new HashSet<Product>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }
        public HashSet<Product> Product { get; set; }
    }
}
