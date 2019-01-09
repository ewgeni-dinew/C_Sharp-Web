using BabyBug.Common.ViewModels.Categories;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Categories.Contracts
{
    public interface ICategoryService: IBaseCloudinaryService
    {
        AllCategoriesModel GetAllProductCategories();

        CreateCategoryModel GetCreateCategoryModel();

        Task CreateCategoryAsync(CreateCategoryModel model);
        
        Task<EditCategoryModel> GetEditCategoryModelAsync(int id);

        Task EditCategoryAsync(int id, EditCategoryModel model);

        Task DeleteCategoryAsync(int id);
    }
}
