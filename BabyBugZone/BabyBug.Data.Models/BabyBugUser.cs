using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyBug.Data.Models
{
    public class BabyBugUser : IdentityUser
    {
        public BabyBugUser()
        {
            this.UserReviews = new HashSet<UserReviews>();
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public ICollection<UserReviews> UserReviews { get; set; }
    }
}
