using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Eventures.ViewModels
{
    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "Username")]
        [RegularExpression("[a-z0-9*_.~-]+")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength =5, ErrorMessage ="Password must be between 5 and 100 symbols.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage ="Passwords do not match!")]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "UCN")]
        [StringLength(10, MinimumLength =10, ErrorMessage ="UCN must be exactly 10 symbols long.")]
        public string UCN { get; set; }
    }
}
