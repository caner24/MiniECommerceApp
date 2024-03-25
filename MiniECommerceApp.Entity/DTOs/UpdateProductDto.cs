using MiniECommerceApp.Entity.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record UpdateProductDto
    {
        public int Id { get; init; }
        public string ProductName { get; init; }
        public double ProductPrice { get; init; }
        public int Amount { get; init; }
        public Size Size { get; init; }
        public List<Category> Categories { get; init; }
    }
}
