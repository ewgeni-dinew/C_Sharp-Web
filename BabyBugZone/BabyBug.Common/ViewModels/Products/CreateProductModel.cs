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
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid Product name.")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public char Gender { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
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
