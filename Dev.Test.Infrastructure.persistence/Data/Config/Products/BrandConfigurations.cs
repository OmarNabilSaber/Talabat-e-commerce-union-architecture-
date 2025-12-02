using Dev.Talabat.Infrastructure.persistence.Data.Config.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Infrastructure.persistence.Data.Config.Products
{
    internal class BrandConfigurations: BaseEntityConfigurations<ProductBrand, int>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);
            builder.Property(b => b.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
