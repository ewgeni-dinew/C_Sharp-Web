using BabyBug.Data.Models.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class ProductType
    {
        public ProductType()
        {
            this.Categories = new HashSet<ProductCategory>();

            this.Sizes = new HashSet<ProductSize>();
        }

        public int Id { get; set; }

        public string Type { get; set; }

        public ICollection<ProductCategory> Categories { get; set; }

        public ICollection<ProductSize> Sizes { get; set; }
    }
}
