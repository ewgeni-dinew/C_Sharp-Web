using BabyBug.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Blog
{
    public class PageEditModel
    {
        public int Id { get; set; }

        [Display(Name = "Author")]
        [StringLength(ModelConstants.AUTHOR_NAME_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.AUTHOR_NAME_MIN)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.BLOG_AUTHOR_RGX_ERROR)]
        public string Author { get; set; }
        
        [Display(Name = "Header")]
        [StringLength(ModelConstants.HEADER_LENGTH_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.HEADER_LENGTH_MIN)]
        [RegularExpression(ModelConstants.ALPHA_NUMERIC_RGX, ErrorMessage = ModelConstants.BLOG_HEADER_RGX_ERROR)]
        public string Header { get; set; }

        [Display(Name = "PageContent")]
        [StringLength(ModelConstants.BLOG_CONTENT_LENGTH_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.BLOG_CONTENT_LENGTH_MIN)]
        public string PageContent { get; set; }

        [Display(Name = "Picture")]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
    }
}
