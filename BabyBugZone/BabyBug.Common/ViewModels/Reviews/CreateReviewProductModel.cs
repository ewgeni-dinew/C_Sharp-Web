using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels
{
    public class CreateReviewProductModel
    {
        public string AuthorName { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public string CreatedOn { get; set; }
    }
}
