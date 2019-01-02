using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.ProductCatalog;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;

namespace BabyBug.Services
{
    public class ProductCatalogService : BaseDbService, IProductCatalogService
    {
        public ProductCatalogService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task<HomeCatalogModel> GetHomeViewModel()
        {
            return await this.GetHomeModelByTypeAsync("Garment");
        }

        public async Task<HomeCatalogModel> GetHomeModelByTypeAsync(string typeName)
        {
            var type = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(typeName));

            var products = this.DbContext
                .Products
                .Where(x => x.TypeId.Equals(type.Id))
                .Select(x => new BaseProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price
                })
                .ToHashSet();

            var model = new HomeCatalogModel()
            {
                Products = products,
                CategoryName = string.Empty,
                ProductType = type.Type,
                FilterModel = new FilterProductsModel()
            };

            return model;
        }

        public async Task<HomeCatalogModel> GetHomeModelByCategory(string categoryName)
        {
            var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Name.Equals(categoryName));

            var productType = await this.DbContext
                .ProductTypes
                .Select(x => new
                {
                    x.Type,
                    x.Id
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(category.ProductTypeId));

            var products = this.DbContext
                .Products
                .Where(x => x.CategoryId.Equals(category.Id))
                .Select(x => new BaseProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price
                })
                .ToHashSet();

            var model = new HomeCatalogModel()
            {
                Products = products,
                CategoryName = category.Name,
                ProductType = productType.Type,
                FilterModel = new FilterProductsModel()
            };

            return model;
        }
    }
}
