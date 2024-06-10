using Microsoft.AspNetCore.Identity;
using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class User : IdentityUser, IEntity
    {
        public Basket Basket { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Comment> Comments { get; set; }
        public string? StripeUserId { get; set; }

    }
}
