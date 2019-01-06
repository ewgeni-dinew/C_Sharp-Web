using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.TypeManagement
{
    public class EditTypeModel
    {
        public ICollection<string> Types { get; set; }

        public string ClassName { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid Type name.")]
        public string OldName { get; set; }

        [Required]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid Type name.")
        public string NewName { get; set; }
    }
}
