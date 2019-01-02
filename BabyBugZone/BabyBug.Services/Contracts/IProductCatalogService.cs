using BabyBug.Common.ViewModels.ProductCatalog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IProductCatalogService
    {
        Task<HomeCatalogModel> GetHomeViewModel();

        Task<HomeCatalogModel> GetHomeModelByTypeAsync(string type);

        Task<HomeCatalogModel> GetHomeModelByCategory(string name);
    }
}
