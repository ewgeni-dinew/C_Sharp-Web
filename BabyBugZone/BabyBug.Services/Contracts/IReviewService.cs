using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels;
using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Reviews;

namespace BabyBug.Services.Contracts
{
    public interface IReviewService
    {
        Task SubmitReviewAsync(string username, ProductDetailsModel model);

        Task<ICollection<AdminReviewBaseModel>> GetAllCreatedReviewsModelAsync();

        Task<AdminReviewDetailsModel> GetReviewDetailsModelAsync(int id);

        Task RemoveReviewAsync(int id);

        Task ApproveReviewAsync(int id);
    }
}
