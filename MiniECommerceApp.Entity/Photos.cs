﻿using MiniECommerceApp.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Entity
{
    public class Photos : IEntity
    {
        public Photos()
        {
            Product = new List<Product>();
        }

        public int Id { get; set; }
        public string PhotosUrl { get; set; }
        public List<Product> Product { get; set; }

    }
}
