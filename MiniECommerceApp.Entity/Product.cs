using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Product
    {
        public Product()
        {
            ShoppingLists = new HashSet<Basket>();
            Invoices = new HashSet<Invoice>();
            ProductPhotos = new HashSet<Photos>();
        }
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required double ProductPrice { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public HashSet<Basket> ShoppingLists { get; set; }
        public HashSet<Invoice> Invoices { get; set; }
        public HashSet<Photos> ProductPhotos { get; set; }
    }
}
