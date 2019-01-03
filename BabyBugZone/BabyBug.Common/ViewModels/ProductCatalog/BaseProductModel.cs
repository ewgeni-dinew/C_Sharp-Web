using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.ProductCatalog
{
    public class BaseProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }

        public string Gender { get; set; }

        public HashSet<string> Sizes { get; set; }
    }
}
