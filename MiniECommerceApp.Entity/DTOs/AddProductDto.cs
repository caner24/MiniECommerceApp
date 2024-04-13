using Microsoft.AspNetCore.Http;
using MiniECommerceApp.Entity.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record AddProductDto
    {
        public string ProductName { get; init; }
        public double ProductPrice { get; init; }
        public int Amount { get; init; }
        public Size Size { get; init; }
        public List<int> CategoriesId { get; init; }
    }
}
