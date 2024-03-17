using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Adress:IEntity
    {
        public int Id { get; set; }
        public User UserId { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string AdressText { get; set; }
        public int ZipCode { get; set; }
        public bool IsDefault { get; set; }
    }
}
