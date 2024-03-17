using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Product : IEntity
    {
        public Product()
        {
            Invoices = new HashSet<Invoice>();
            ProductPhotos = new HashSet<Photos>();
        }
        public int Id { get; set; }
        public  string ProductName { get; set; }
        public  double ProductPrice { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public HashSet<Invoice> Invoices { get; set; }
        public HashSet<Photos> ProductPhotos { get; set; }
    }
}
