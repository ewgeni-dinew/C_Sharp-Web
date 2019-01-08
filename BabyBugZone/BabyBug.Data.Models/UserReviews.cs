using BabyBug.Data.Models.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class UserReviews
    {
        public int ReviewId { get; set; }

        public Review Review { get; set; }

        public string UserId { get; set; }

        public BabyBugUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
