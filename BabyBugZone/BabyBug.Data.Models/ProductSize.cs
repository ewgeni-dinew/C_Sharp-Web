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
            this.GarmentSpecifications = new HashSet<GarmentSpecification>();

            this.ShoeSpecifications = new HashSet<ShoeSpecification>();

            this.AccessorySpecifications = new HashSet<AccessorySpecification>();
        }

        public int Id { get; set; }

        public string Value { get; set; }

        public ICollection<GarmentSpecification> GarmentSpecifications { get; set; }

        public ICollection<ShoeSpecification> ShoeSpecifications { get; set; }

        public ICollection<AccessorySpecification> AccessorySpecifications { get; set; }
    }
}
