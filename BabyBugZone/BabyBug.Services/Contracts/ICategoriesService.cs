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

        ICollection<BaseCategoryModel> GetAllGarmentCategories();

        Task CreateCategoryAsync(CreateCategoryModel model);

        Task<EditCategoryModel> GetEditCategoryModelAsync(int id);

        Task<DeleteCategoryModel> GetDeleteCategoryModelAsync(int id);

        Task EditCategoryAsync(int id, EditCategoryModel model);

        Task DeleteCategoryAsync(int id);
       
    }
}
