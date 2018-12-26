using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Orders
{
    public class UserDataModel
    {
        public string Username { get; set; }

        public int OrderId { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        [StringLength(15, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        public string Address { get; set; }

        public string PaymentType { get; set; }

        public string DeliveryType { get; set; }

        public string DeliveryDestination { get; set; }
    }
}
