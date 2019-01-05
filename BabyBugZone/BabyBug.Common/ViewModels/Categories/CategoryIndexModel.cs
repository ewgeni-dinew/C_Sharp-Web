using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Categories
{
    public class CategoryIndexModel
    {
        public string TypeName { get; set; }

        public ICollection<BaseCategoryModel> SubCategories { get; set; }
    }
}
