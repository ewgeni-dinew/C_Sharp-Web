using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.ProductCatalog
{
    public class PaginationModel
    {
        public IEnumerable<BaseProductModel> DisplayProducts { get; set; }

        public int CurrentPage { get; set; }

        public int AllPages { get; set; }
    }
}
