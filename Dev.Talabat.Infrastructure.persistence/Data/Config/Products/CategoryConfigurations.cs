using Dev.Talabat.Infrastructure.persistence.Data.Config.Base;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Infrastructure.persistence.Data.Config.Products
{
    internal class CategoryConfigurations : BaseEntityConfigurations<ProductCategory, int>
    {
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
