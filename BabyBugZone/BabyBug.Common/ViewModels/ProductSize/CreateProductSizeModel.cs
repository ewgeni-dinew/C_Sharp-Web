using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.ProductSize
{
    public class CreateProductSizeModel
    {
        [Required]
        [Display(Name = "Sizes")]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Values { get; set; }

        public HashSet<string> Types { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z0-9\,\;\s\-\(\)]^+$", ErrorMessage = "Invalid Type name.")]        
        public string Type { get; set; }
    }
}
