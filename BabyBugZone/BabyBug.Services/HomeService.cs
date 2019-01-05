using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Blog;
using BabyBug.Common.ViewModels.Home;
using BabyBug.Common.ViewModels.ProductCatalog;
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
            //todo remove
            //await this.DbContext.SaveChangesAsync();

            var blogPages = this.DbContext
                .BlogPages
                .Where(x => x.IsDeleted == false)
                .Select(x => new HomePageBlogModel
                {
                    Author = x.Author,
                    Heading = x.Header,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                    ImageUrl = x.ImageUrl,
                    PageId = x.Id
                })
                .OrderByDescending(x => x.CreatedOn)
                .Take(3)
                .ToList();

            var latestProducts = this.DbContext
                .Products
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new BaseLatestProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    Gender = x.Gender.ToString(),
                    ProductType = x.TypeId.ToString(),
                })
                .Take(10)
                .ToList();

            foreach (var item in latestProducts)
            {
                var typeId = int.Parse(item.ProductType);

                item.ProductType = await GetProductTypeName(typeId);
            }

            var model = new IndexViewModel
            {
                BlogPages = blogPages,
                LatestProducts = latestProducts,
            };

            return model;
        }

        private async Task<string> GetProductTypeName(int? typeId)
        {
            var type = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Id.Equals(typeId));

            return type.Type;
        }
    }
}
