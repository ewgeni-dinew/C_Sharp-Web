using BabyBug.Common.Constants;
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
        [StringLength(ModelConstants.TYPE_VALUE_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.TYPE_VALUE_MAX)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.TYPE_VALUE_RGX_ERROR)]
        public string Name { get; set; }

        public HashSet<string> Types { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
