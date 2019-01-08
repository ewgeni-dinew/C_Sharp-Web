using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Reviews
{
    public class AdminReviewBaseModel
    {
        public string ProductName { get; set; }

        public int ProductId { get; set; }

        public string AuthorName { get; set; }

        public int ReviewId { get; set; }

        public string CreatedOn { get; set; }
    }
}
