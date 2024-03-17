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
        public string UserId { get; set; }
        public User User { get; set; }
        public HashSet<Product> Products { get; set; }
    }
}
