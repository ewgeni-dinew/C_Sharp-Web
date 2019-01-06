using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class EditProductModel
    {
        public int Id { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid Product name.")]
        public string Name { get; set; }

        public char Gender { get; set; }

        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string CreatedOn { get; set; }

        public HashSet<string> CategoryNames { get; set; }

        public string CategoryName { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
    }
}
