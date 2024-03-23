using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniECommerceApp.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerceApp.Data.Concrete.Configuration
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        void IEntityTypeConfiguration<ProductDetail>.Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.HasKey(x => x.ProductId);
            builder.HasOne(x => x.Product).WithOne(x => x.ProductDetail).HasForeignKey<ProductDetail>(x => x.ProductId);
        }
    }
}
