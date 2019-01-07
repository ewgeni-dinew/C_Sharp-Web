using BabyBug.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Categories
{
    public class EditCategoryModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(ModelConstants.CATEGORY_LENGTH_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.CATEGORY_LENGTH_MIN)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.CATEGORY_NAME_RGX_ERROR)]
        public string Name { get; set; }
        
        [Display(Name = "Picture")]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }

        public ICollection<string> CategoryTypes { get; set; }

        [Required]
        public string CategoryType { get; set; }
    }
}
