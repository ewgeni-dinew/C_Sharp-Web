using BabyBug.Common.ViewModels.ProductSize;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface ISizeService
    {
        BabyBugDbContext DbContext { get; set; }

        Task<ICollection<BaseProductSizeModel>> GetAllProductSizesAsync();

        CreateProductSizeModel GetCreateSizeModel();

        Task<BaseProductSizeModel> GetBaseSizeModelAsync(int id);

        Task CreateSizeAsync(CreateProductSizeModel model);

        Task DeleteSizeAsync(int id);

        Task<ProductManageSizesModel> GetCurrentProductSizeDetails(int productId, int typeId);

        Task AddQuantityToProductAsync(int id, ProductManageSizesModel model);

        Task RemoveQuantityFromProductAsync(int id, ProductManageSizesModel model);

        Task EditSizeAsync(BaseProductSizeModel model);
    }
}
