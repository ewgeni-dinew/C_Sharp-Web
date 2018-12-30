using BabyBug.Common.ViewModels.Categories;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Categories.Contracts
{
    public interface ICategoriesService
    {
        BabyBugDbContext DbContext { get; set; }

        AllCategoriesModel GetAllGarmentCategories();

        Task CreateCategoryAsync(CreateCategoryModel model);
        
        Task<EditCategoryModel> GetEditGarmentCatModelAsync(int id);

        Task<EditCategoryModel> GetEditShoeCatModelAsync(int id);

        Task<EditCategoryModel> GetEditAccessoryCatModelAsync(int id);

        Task EditGarmentCategoryAsync(int id, EditCategoryModel model);

        Task EditShoeCategoryAsync(int id, EditCategoryModel model);

        Task EditAccessoryCategoryAsync(int id, EditCategoryModel model);

        Task DeleteGarmentCategoryAsync(int id);

        Task DeleteShoeCategoryAsync(int id);

        Task DeleteAccessoryCategoryAsync(int id);
    }
}
