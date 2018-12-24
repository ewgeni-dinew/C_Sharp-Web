using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace BabyBug.Data.Models
{
    public class BabyBugUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string City { get; set; }

        public string Address { get; set; }
    }
}
