using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Talabat.Domain.Entities.Products
{
    public class Product: BaseAuditableEntity<int>
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        // Navigation Properties
        public ProductBrand? ProductBrand { get; set; }
        public ProductCategory? ProductCategory { get; set; }
    }
}
