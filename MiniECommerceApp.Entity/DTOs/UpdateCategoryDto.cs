using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record UpdateCategoryDto
    {
        public int Id { get; init; }

        public string CategoryName { get; init; }
    }
}
