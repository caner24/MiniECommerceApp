using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Invoice : IEntity
    {
        public Invoice()
        {
            Product = new List<Product>();
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Product> Product { get; set; }
        public Guid InvoiceNo { get; set; }

    }
}
