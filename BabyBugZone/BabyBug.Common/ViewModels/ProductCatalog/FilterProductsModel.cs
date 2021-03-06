﻿using System.Collections.Generic;

namespace BabyBug.Common.ViewModels.ProductCatalog
{
    public class FilterProductsModel
    {
        public HashSet<string> Sizes { get; set; }        

        public HashSet<string> ChosenSizes { get; set; }

        public string PriceRange { get; set; }
        
        public string Gender { get; set; }        
    }
}