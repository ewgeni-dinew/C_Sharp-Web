using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Blog;

namespace BabyBug.Services.Contracts
{
    public interface IBlogService
    {
        Task CreateBlogPageAsync(CreatePageModel model);

        Task<PageEditModel> GetEditPageModelByIdAsync(int id);

        Task EditBlogPageAsync(int id, PageEditModel model);

        Task DeleteBlogAsync(int id);

        Task<PageDetailsModel> GetBlogDetailsAsync(int id);

        ICollection<HomePageBlogModel> GetBasePageModelCollection();
    }
}
