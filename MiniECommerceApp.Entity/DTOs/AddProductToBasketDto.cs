﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity.DTOs
{
    public record AddProductToBasketDto
    {
        public string UserId { get; init; }

        public List<int> ProdId { get; init; }

    }
}
