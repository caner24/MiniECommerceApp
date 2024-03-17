using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Photos : IEntity
    {
        public Photos()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public required string PhotosUrl { get; set; }
        public HashSet<Product> Product { get; set; }

    }
}
