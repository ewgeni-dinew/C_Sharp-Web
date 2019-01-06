using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.TypeManagement
{
    public class CreateTypeModel
    {
        [Required]
        [Display(Name = "Name")]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid Type name.")]
        public string Name { get; set; }

        public HashSet<string> Types { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
