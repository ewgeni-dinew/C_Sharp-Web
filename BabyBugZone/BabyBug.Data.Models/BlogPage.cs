using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class BlogPage
    {
        public BlogPage()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        public int Id { get; set; }

        public string Author { get; set; }

        public string Header { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public string ImageId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
