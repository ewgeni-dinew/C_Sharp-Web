using System;
using System.Collections.Generic;

namespace BabyBug.Common.ViewModels.ProductCatalog
{
    public class HomeCatalogModel
    {
        public IEnumerable<BaseProductModel> AllProducts { get; set; }
        
        public HashSet<string> ProductTypes { get; set; }

        public HashSet<string> CategoryNames { get; set; }

        public string ProductType { get; set; }

        public string CategoryName { get; set; }

        public FilterProductsModel FilterModel { get; set; }

        public PaginationModel PaginationModel { get; set; }
    }
}
