using BabyBug.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels
{
    public class CreateReviewProductModel
    {
        [Display(Name ="Name")]
        [StringLength(ModelConstants.AUTHOR_NAME_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.AUTHOR_NAME_MIN)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.REVIEW_NAME_RGX_ERROR)]
        public string AuthorName { get; set; }

        [StringLength(ModelConstants.CONTENT_DESCRIPTION_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.CONTENT_DESCRIPTION_MIN)]
        [RegularExpression(ModelConstants.ALPHA_NUMERIC_RGX, ErrorMessage = ModelConstants.REVIEW_CONTENT_RGX_ERROR)]
        public string Content { get; set; }

        public int Rating { get; set; }

        public string CreatedOn { get; set; }
    }
}
