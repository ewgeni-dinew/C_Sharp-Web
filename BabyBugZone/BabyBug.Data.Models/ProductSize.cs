using BabyBug.Data.Models.ProductSpecifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class ProductSize
    {
        public ProductSize()
        {
            this.ProductSpecifications = new HashSet<ProductSpecification>();
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public ProductType ProductType { get; set; }

        public int ProductTypeId { get; set; }

        public ICollection<ProductSpecification> ProductSpecifications { get; set; }
    }
}
