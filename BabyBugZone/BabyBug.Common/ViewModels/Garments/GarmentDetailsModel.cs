﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class GarmentDetailsModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public string CreatedOn { get; set; }

        //TODO:
        //add sizes dropdown
        //add quantity 
    }
}
