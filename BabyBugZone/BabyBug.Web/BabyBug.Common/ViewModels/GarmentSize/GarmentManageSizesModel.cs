﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BabyBug.Common.ViewModels.GarmentSize
{
    public class GarmentManageSizesModel
    {
        public int GarmentId { get; set; }

        public IDictionary<string, uint> CurrentSizes { get; set; }

        public HashSet<string> AllGarmentSizes { get; set; }

        public string ChoosenSize { get; set; }

        [Required]
        [Display(Name = "Sizes")]
        [Range(1, 100)]
        public uint InputQuantity { get; set; }
    }
}