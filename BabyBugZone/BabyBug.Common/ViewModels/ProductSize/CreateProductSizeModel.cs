using BabyBug.Common.Constants;
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
        [StringLength(ModelConstants.SIZE_VALUES_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.SIZE_VALUES_MIN)]
        public string Values { get; set; }

        public HashSet<string> Types { get; set; }

        [Required]
        [RegularExpression(ModelConstants.ALPHA_NUMERIC_RGX, ErrorMessage = ModelConstants.SIZE_VALUE_RGX_ERROR)]        
        public string Type { get; set; }
    }
}
