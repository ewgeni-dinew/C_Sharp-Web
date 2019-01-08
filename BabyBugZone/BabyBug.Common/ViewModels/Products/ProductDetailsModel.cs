using BabyBug.Common.ViewModels.Reviews;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class ProductDetailsModel
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string CategoryName { get; set; }

        public string Name { get; set; }

        public int Rating { get; set; }

        public char Gender { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public string CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public HashSet<string> AvailableSizes { get; set; }        
        
        public string Size { get; set; }
        
        public uint Quantity { get; set; }

        public ICollection<BaseDisplayProductReviewModel> ProductReviews { get; set; }

        public CreateReviewProductModel ReviewModel { get; set; }
    }
}
