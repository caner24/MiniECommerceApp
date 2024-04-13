using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record CreateInvoiceDto
    {
        public string UserId { get; init; }
        public List<int> ProductId { get; init; }
        public List<int> Amount { get; init; }
    }
}
