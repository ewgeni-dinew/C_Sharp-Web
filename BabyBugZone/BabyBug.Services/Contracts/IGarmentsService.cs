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

        Task CreateGarmentAsync(CreateGarmentModel model);

        Task<GarmentDetailsModel> GetDetailsAsync(int id);
    }
}
