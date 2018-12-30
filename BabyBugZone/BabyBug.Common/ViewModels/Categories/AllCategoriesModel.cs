using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Categories
{
    public class AllCategoriesModel
    {
        public ICollection<BaseCategoryModel> GarmentCategories { get; set; }

        public ICollection<BaseCategoryModel> ShoeCategories { get; set; }

        public ICollection<BaseCategoryModel> AccessoryCategories { get; set; }
    }
}
