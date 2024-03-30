using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Concrete.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Navigation(e => e.ProductPhotos).AutoInclude();
            builder.Navigation(e => e.Categories).AutoInclude();
            builder.Property(x => x.ConcurrencyToken).IsRowVersion();
        }
    }
}
