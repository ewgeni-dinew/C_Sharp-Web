using BabyBug.Common.Constants;
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
        [StringLength(ModelConstants.TYPE_VALUE_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.TYPE_VALUE_MAX)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.TYPE_VALUE_RGX_ERROR)]
        public string OldName { get; set; }

        [Required]
        [StringLength(ModelConstants.TYPE_VALUE_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.TYPE_VALUE_MAX)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.TYPE_VALUE_RGX_ERROR)]
        public string NewName { get; set; }
    }
}
