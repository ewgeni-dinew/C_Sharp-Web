using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Blog
{
    public class HomePageBlogModel
    {
        public string Author { get; set; }

        public string Heading { get; set; }

        public string Content { get; set; }

        public string CreatedOn { get; set; }

        public string ImageUrl { get; set; }

        public int PageId { get; set; }
    }
}
