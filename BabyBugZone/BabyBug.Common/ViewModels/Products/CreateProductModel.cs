using BabyBug.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class CreateProductModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(ModelConstants.PRODUCT_NAME_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.PRODUCT_NAME_MIN)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.PRODUCT_NAME_RGX_ERROR)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public char Gender { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(ModelConstants.DESTINATION_NAME_LENGTH_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.DESTINATION_NAME_LENGTH_MIN)]
        public string Description { get; set; }

        [Display(Name = "CategoryNames")]
        public HashSet<string> CategoryNames { get; set; }

        [Required]
        [Display(Name = "CategoryName")]
        public string CategoryName { get; set; }

        public HashSet<string> ProductTypes { get; set; }

        public string ProductType { get; set; }

        [Required]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Picture")]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
    }
}
