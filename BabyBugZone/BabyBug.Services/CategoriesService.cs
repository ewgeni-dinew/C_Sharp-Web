using BabyBug.Common.ViewModels.Categories;
using BabyBug.Data.Models;
using BabyBug.Services.Categories.Contracts;
using BabyBug.Web.Models.Categories;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class CategoriesService: ICategoriesService
    {
        public CategoriesService(
            BabyBugDbContext DbContext
            //TODO add automapper
            )
        {
            this.DbContext = DbContext;
        }

        public BabyBugDbContext DbContext { get; set; }

        public ICollection<BaseCategoryModel> GetAllGarmentCategories()
        {
            var collection = this.DbContext
                .GarmentCategories
                .ToList();

            var categoryList = new List<BaseCategoryModel>();

            foreach (var c in collection)
            {
                var temp = new BaseCategoryModel
                {
                    Name = c.Name
                };

                categoryList.Add(temp);
            }

            return categoryList;
        }

        public async Task CreateCategoryAsync(CreateCategoryModel model)
        {
            var category = new GarmentCategory
            {
                Name=model.Name,
            };

            await this.DbContext.GarmentCategories.AddAsync(category);
            await this.DbContext.SaveChangesAsync();
        }
    }
}
