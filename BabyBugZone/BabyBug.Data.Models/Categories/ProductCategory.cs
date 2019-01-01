using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Enums;
using BabyBug.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Data.Models.Categories
{
    public class ProductCategory : ICategory
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public ProductType ProductType { get; set; }

        public int ProductTypeId { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
