using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record AddCommentsToProductDto
    {
        public int ProductId { get; init; }
        public string CommentText { get; init; }
    }
}
