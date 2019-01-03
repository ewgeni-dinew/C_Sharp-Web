using System.Collections.Generic;

namespace BabyBug.Common.ViewModels.ProductCatalog
{
    public class FilterProductsModel
    {
        public HashSet<string> Sizes { get; set; }

        public HashSet<string> ChosenSizes { get; set; }

        public string Gender { get; set; }

        public int StartPrice { get; set; }

        public int EndPrice { get; set; }

        public string CategoryName { get; set; }

        public string ProductType { get; set; }
    }
}