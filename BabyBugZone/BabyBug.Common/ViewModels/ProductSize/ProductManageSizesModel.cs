using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.ProductSize
{
    public class ProductManageSizesModel
    {
        public int ProductId { get; set; }

        public int TypeId { get; set; }

        public IDictionary<string, uint> CurrentSizes { get; set; }

        public HashSet<string> AllProductSizes { get; set; }

        [Required]
        public string ChoosenSize { get; set; }

        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Sizes")]
        [Range(1, 100)]
        public uint InputQuantity { get; set; }
    }
}
