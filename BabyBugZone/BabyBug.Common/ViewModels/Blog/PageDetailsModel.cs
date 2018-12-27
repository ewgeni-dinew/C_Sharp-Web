using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Blog
{
    public class PageDetailsModel
    {
        public int Id { get; set; }

        public string Author { get; set; }

        public string Header { get; set; }

        public string CreatedOn { get; set; }

        public string Contect { get; set; }

        public string ImageUrl { get; set; }
    }
}
