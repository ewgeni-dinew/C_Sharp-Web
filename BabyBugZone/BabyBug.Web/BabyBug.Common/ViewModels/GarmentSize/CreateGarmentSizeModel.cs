using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.GarmentSize
{
    public class CreateGarmentSizeModel
    {
        [Required]
        [Display(Name = "Sizes")]
        [StringLength(35, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        public string Values { get; set; }
    }
}
