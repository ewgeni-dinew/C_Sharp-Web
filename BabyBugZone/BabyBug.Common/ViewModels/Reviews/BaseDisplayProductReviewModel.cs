using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Reviews
{
    public class BaseDisplayProductReviewModel
    {
        public string Author { get; set; }

        public string CreatedOn { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }
    }
}
