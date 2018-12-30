using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Enums;
using BabyBug.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Data.Models.Categories
{
    public class GarmentCategory : ICategory
    {
        public GarmentCategory()
        {
            this.Garments = new HashSet<Garment>();

            this.CategoryType = CategoryType.Garment;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public CategoryType CategoryType { get; set; }

        public ICollection<Garment> Garments { get; set; }
    }
}
