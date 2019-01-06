using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Orders
{
    public class UserDataModel
    {
        [Required]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid Username.")]
        public string Username { get; set; }

        public int OrderId { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid name.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid name.")]   
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]        
        [RegularExpression("^[0-9]+$", ErrorMessage = "Invalid {0} number.")]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid {0} name.")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [RegularExpression(@"^[a-zA-Z0-9\.\-\s\,]*$", ErrorMessage = "Invalid {0} name.")]
        public string Address { get; set; }

        public HashSet<string> PaymentTypes { get; set; }

        [Required]
        public string PaymentType { get; set; }

        public HashSet<string> DeliveryTypes { get; set; }

        [Required]
        public string DeliveryType { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]    
        public string DeliveryDestination { get; set; }
    }
}
