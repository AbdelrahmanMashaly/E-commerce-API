using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Repository.Data.Config
{
    internal class ProductConfiguring : IEntityTypeConfiguration<Product>
    {
        void IEntityTypeConfiguration<Product>.Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(P => P.Name).HasMaxLength(100);
            builder.Property(P => P.Price).HasColumnType("decimal(18,2)");
            builder.HasOne(P => P.ProductType).WithMany();
            builder.HasOne(P => P.ProductBrand).WithMany();
        }
    }
}
