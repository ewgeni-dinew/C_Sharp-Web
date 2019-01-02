using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.ProductCatalog
{
    public class HomeCatalogModel
    {
        public ICollection<BaseProductModel> Products { get; set; }

        public string ProductType { get; set; }

        public string CategoryName { get; set; }

        public FilterProductsModel FilterModel { get; set; }
    }
}
