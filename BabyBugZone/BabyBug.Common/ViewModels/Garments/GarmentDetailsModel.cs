using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class GarmentDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public char Gender { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public string CreatedOn { get; set; }
        
        public string CategoryName { get; set; }

        public string ImageUrl { get; set; }

        public HashSet<string> AvailableSizes { get; set; }

        public string Size { get; set; }

        [Display(Name="Quantity")]
        [Range(0,5, ErrorMessage ="The quantity must be between 0 and 5!")]
        public uint Quantity { get; set; }
    }
}
