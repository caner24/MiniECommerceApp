using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record AddCategoryDto
    {
        public string CategoryName { get; init; }
    }
}
