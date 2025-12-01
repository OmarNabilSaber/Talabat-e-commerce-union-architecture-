using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Domain.Entities.Product
{
    public class ProductCategory: BaseEntity<int>
    {
        public required string Name { get; set; }
    }
}
