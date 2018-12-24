using BabyBug.Common.ViewModels.Garments;
using BabyBugZone.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services.Contracts
{
    public interface IGarmentsService
    {
        BabyBugDbContext DbContext { get; set; }

        CreateGarmentModel GetGarmentCreateModel();

        Task<DeleteGarmentModel> GetDeleteModelAsync(int id);

        Task CreateGarmentAsync(CreateGarmentModel model);

        Task DeleteGarmentAsync(int id);

        Task<GarmentDetailsModel> GetDetailsModelAsync(int id);

        Task<EditGarmentModel> GetEditModelAsync(int id);

        Task EditGarmentAsync(int id, EditGarmentModel model);
    }
}
