using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Enums;
using BabyBug.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Data.Models.Categories
{
    public class ShoeCategory: ICategory
    {
        public ShoeCategory()
        {
            this.Shoes = new HashSet<Shoe>();

            this.CategoryType = CategoryType.Shoe;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public CategoryType CategoryType { get; set; }

        public ICollection<Shoe> Shoes { get; set; }
    }
}
