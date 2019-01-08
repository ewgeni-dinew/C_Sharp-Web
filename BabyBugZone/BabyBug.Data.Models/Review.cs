using BabyBug.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Data.Models
{
    public class Review
    {
        public Review()
        {
            this.Status = ReviewStatus.Created;

            this.CreatedOn = DateTime.UtcNow;

            this.UserReviews = new HashSet<UserReviews>();
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedOn { get; set; }

        public ReviewStatus Status { get; set; }

        public ICollection<UserReviews> UserReviews { get; set; }
    }
}
