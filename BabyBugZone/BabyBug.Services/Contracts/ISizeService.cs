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

        ICollection<BaseProductSizeModel> GetAllGarmentSizes();

        CreateProductSizeModel GetCreateSizeModel();

        Task<BaseProductSizeModel> GetBaseSizeModelAsync(int id);

        Task CreateSizeAsync(CreateProductSizeModel model);

        Task DeleteSizeAsync(int id);

        Task<ProductManageSizesModel> GetCurrentGarmentSizeDetails(int id);

        Task AddQuantityToGarmentAsync(int id, ProductManageSizesModel model);

        Task RemoveQuantityFromGarmentAsync(int id, ProductManageSizesModel model);

        Task EditSizeAsync(BaseProductSizeModel model);
    }
}
