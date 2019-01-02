using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Blog;
using BabyBug.Common.ViewModels.Home;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;

namespace BabyBug.Services
{
    public class HomeService : BaseDbService, IHomeService
    {
        public HomeService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task<IndexViewModel> GetIndexModelAsync()
        {
            //to be removed
            var blog = await this.DbContext
                .BlogPages
                .FirstOrDefaultAsync();

            var blogPages = this.DbContext
                .BlogPages
                .Where(x=>x.IsDeleted==false)
                .Select(x => new HomePageBlogModel
                {
                    Author = x.Author,
                    Heading = x.Header,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    ImageUrl=x.ImageUrl,
                    PageId =x.Id
                })
                .OrderBy(x => x.CreatedOn)
                .Take(3)
                .ToList();

            var model = new IndexViewModel
            {
                BlogPages = blogPages,
            };

            return model;
        }
    }
}
