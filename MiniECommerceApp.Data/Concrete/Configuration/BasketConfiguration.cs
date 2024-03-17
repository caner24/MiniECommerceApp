﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerceApp.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Concrete.Configuration
{
    public class BasketConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.HasOne(x => x.User).WithOne(x => x.Basket).HasForeignKey<Basket>(x => x.UserId);

            builder.Property(x => x.Products)
          .HasConversion(
              v => JsonConvert.SerializeObject(v),
              v => JsonConvert.DeserializeObject<HashSet<Product>>(v),
              new ValueComparer<HashSet<Product>>(
                  (c1, c2) => c1.SequenceEqual(c2),
                  c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                  c => new HashSet<Product>(c))
          );
        }
    }
}
