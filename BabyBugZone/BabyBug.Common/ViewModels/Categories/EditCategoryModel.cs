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
        [RegularExpression("^[A-z]+$", ErrorMessage = "Invalid Category name.")]
        public string Name { get; set; }
        
        [Display(Name = "Picture")]
        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }

        public ICollection<string> CategoryTypes { get; set; }

        [Required]
        public string CategoryType { get; set; }
    }
}
