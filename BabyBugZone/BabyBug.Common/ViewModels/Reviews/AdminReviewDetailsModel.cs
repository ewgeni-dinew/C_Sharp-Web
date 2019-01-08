using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Reviews
{
    public class AdminReviewDetailsModel
    {
        [Display(Name = "Product name")]
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public int ReviewId { get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }

        [Display(Name = "Author name")]
        public string AuthorName { get; set; }

        public string Content { get; set; }

        [Display(Name = "Created on")]
        public string CreatedOn { get; set; }
    }
}
