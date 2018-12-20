using BabyBug.Common.ViewModels.GarmentSize;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IGarmentSizeService
    {
        BabyBugDbContext DbContext { get; set; }

        ICollection<BaseGarmentSizeModel> GetAllGarmentSizes();

        Task CreateSizeAsync(CreateGarmentSizeModel model);

        Task DeleteSizeAsync(int id);

        Task<GarmentManageSizesModel> GetCurrentGarmentSizeDetails(int id);

        Task AddQuantityToGarmentAsync(int id, GarmentManageSizesModel model);

        Task RemoveQuantityFromGarmentAsync(int id, GarmentManageSizesModel model);
    }
}
