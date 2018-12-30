using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models.ProductSpecifications
{
    public class AccessorySpecification : IProductSpecification
    {
        public Accessory Product { get; set; }

        public int ProductId { get; set; }

        public ProductSize ProductSize { get; set; }

        public int ProductSizeId { get; set; }

        public uint Quantity { get; set; }
    }
}
