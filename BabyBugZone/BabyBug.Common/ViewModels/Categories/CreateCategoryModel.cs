using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Categories
{
    public class CreateCategoryModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Picture")]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }

        public ICollection<string> CategoryTypes { get; set; }

        public string CategoryType { get; set; }
    }
}
