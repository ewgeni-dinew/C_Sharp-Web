using BabyBug.Common.ViewModels.Categories;
using BabyBug.Web.Models.Categories;
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
    }
}
