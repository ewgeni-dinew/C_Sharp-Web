using BabyBug.Common.ViewModels.Categories;
using BabyBug.Data.Models;
using BabyBug.Services.Categories.Contracts;
using BabyBug.Web.Models.Categories;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class CategoriesService : ICategoriesService
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
                Name = model.Name,
            };

            await this.DbContext.GarmentCategories.AddAsync(category);
            await this.DbContext.SaveChangesAsync();
        }

        public ModifyCategoryModel GetEditCategoryModel()
        {
            var categories = this.DbContext
                .GarmentCategories
                .Select(x => x.Name)
                .ToHashSet();

            var model = new ModifyCategoryModel
            {
                CategoryNames = categories,
            };

            return model;
        }

        public ModifyCategoryModel GetDeleteCategoryModel()
        {
            var categories = this.DbContext
                .GarmentCategories
                .Select(x => x.Name)
                .ToHashSet();

            var model = new ModifyCategoryModel
            {
                CategoryNames = categories,
            };

            return model;
        }

        public async Task EditCategoryAsync(ModifyCategoryModel model)
        {
            var category =await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x=>x.Name.Equals(model.CategoryName));

            category.Name = model.NewName;

            //TODO validate picture

            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(ModifyCategoryModel model)
        {
            if (!model.Consent)
            {
                return;
            }

            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x=>x.Name.Equals(model.CategoryName));

            this.DbContext.GarmentCategories.Remove(category);

            await this.DbContext.SaveChangesAsync();
        }
    }
}
