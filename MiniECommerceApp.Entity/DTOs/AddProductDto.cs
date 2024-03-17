using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record AddProductDto
    {
        public required string ProductName { get; init; }
        public required double ProductPrice { get; init; }
        public ProductDetail ProductDetail { get; init; }
        public HashSet<Invoice> Invoices { get; init; }
        public HashSet<Photos> ProductPhotos { get; init; }
    }
}
