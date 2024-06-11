using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record AddProductToBasketDto
    {
        public int ProdId { get; init; }

        public int Amount { get; init; }
    }
}
