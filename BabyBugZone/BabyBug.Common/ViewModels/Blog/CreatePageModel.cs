﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Blog
{
    public class CreatePageModel
    {
        [Required]
        [Display(Name = "Author")]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Header")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string Header { get; set; }

        [Required]
        [Display(Name = "Content")]
        [StringLength(5000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string Content { get; set; }

        [Required]
        [Display(Name = "Picture")]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
    }
}