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
            Invoices = new List<Invoice>();
            ProductPhotos = new List<Photos>();
            Categories = new List<Category>();
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string StripeProductId { get; set; }
        public double ProductPrice { get; set; }
        public int Amount { get; set; }
        public byte[] ConcurrencyToken { get; set; }
        public ProductDetail ProductDetail { get; set; }
        public List<Category> Categories { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Photos> ProductPhotos { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
