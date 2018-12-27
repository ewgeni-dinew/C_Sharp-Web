using BabyBug.Common.ViewModels.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Home
{
    public class IndexViewModel
    {
        public ICollection<HomePageBlogModel> BlogPages { get; set; }
    }
}
