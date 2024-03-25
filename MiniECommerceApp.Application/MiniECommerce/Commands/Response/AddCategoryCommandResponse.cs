using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Application.MiniECommerce.Commands.Response
{
    public class AddCategoryCommandResponse
    {
        public bool IsAdded { get; set; }

        public Category Category { get; set; }
    }
}
