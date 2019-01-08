using BabyBug.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Orders
{
    public class UserDataModel
    {
        public int OrderId { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        [StringLength(ModelConstants.USER_NAME_LENGTH_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.USER_NAME_LENGTH_MIN)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.USER_NAME_RGX_ERROR)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        [StringLength(ModelConstants.USER_NAME_LENGTH_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.USER_NAME_LENGTH_MIN)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.USER_NAME_RGX_ERROR)]   
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Telephone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(ModelConstants.PHONE_VALUE_MAX, ErrorMessage = ModelConstants.ERROR_MSG , MinimumLength = ModelConstants.PHONE_VALUE_MIN)]        
        [RegularExpression(ModelConstants.NUMERICS_RGX, ErrorMessage = ModelConstants.PHONE_VALUE_RGX_ERROR)]
        public string Telephone { get; set; }

        [Required]
        [Display(Name = "City")]
        [StringLength(20, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = 4)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.CITY_NAME_RGX_ERROR)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(30, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = 8)]
        [RegularExpression(ModelConstants.ALPHA_NUMERIC_RGX, ErrorMessage = ModelConstants.ADDRESS_NAME_RGX_ERROR)]
        public string Address { get; set; }

    }
}
