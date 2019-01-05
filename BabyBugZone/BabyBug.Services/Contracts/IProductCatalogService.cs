using BabyBug.Common.ViewModels.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IProductCatalogService
    {
        Task<HomeCatalogModel> GetHomeViewModelAsync();

        Task<HomeCatalogModel> GetHomeModelByTypeAsync(string type);

        Task<HomeCatalogModel> GetHomeModelByTypeAsync(string type, string gender);

        Task<HomeCatalogModel> GetHomeModelByCategoryAsync(string name);

        Task<HomeCatalogModel> GetHomeModelByCriteriaAsync(HomeCatalogModel model);

        Task<HomeCatalogModel> SetPaginationModelAsync(int pageIndex, HomeCatalogModel model);

        Task<HomeCatalogModel> GetHomeModelByGenderAsync(string gender);

    }
}
