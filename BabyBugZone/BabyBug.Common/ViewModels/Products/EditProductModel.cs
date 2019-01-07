using BabyBug.Common.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class EditProductModel
    {
        public int Id { get; set; }

        [StringLength(ModelConstants.PRODUCT_NAME_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.PRODUCT_NAME_MIN)]
        [RegularExpression(ModelConstants.ALPHABETS_RGX, ErrorMessage = ModelConstants.PRODUCT_NAME_RGX_ERROR)]
        public string Name { get; set; }

        public char Gender { get; set; }

        [StringLength(ModelConstants.PRODUCT_DESCRIPTION_MAX, ErrorMessage = ModelConstants.ERROR_MSG, MinimumLength = ModelConstants.PRODUCT_DESCRIPTION_MIN)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string CreatedOn { get; set; }

        public HashSet<string> CategoryNames { get; set; }

        public string CategoryName { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Picture { get; set; }
    }
}
