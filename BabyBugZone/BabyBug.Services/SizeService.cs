using BabyBug.Common.ViewModels.ProductSize;
using BabyBug.Data.Models;
using BabyBug.Data.Models.ProductSpecifications;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class SizeService : BaseDbService, ISizeService
    {
        public SizeService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task<ICollection<BaseProductSizeModel>> GetAllProductSizesAsync()
        {
            var productSizes = this.DbContext
                .ProductSizes
                .ToHashSet();

            var collection = new HashSet<BaseProductSizeModel>();

            foreach (var size in productSizes.OrderBy(x => x.ProductTypeId))
            {
                var type = await this.DbContext
                    .ProductTypes
                    .FirstOrDefaultAsync(x => x.Id.Equals(size.ProductTypeId));

                var model = new BaseProductSizeModel
                {
                    Id = size.Id,
                    Name = size.Value,
                    Type = type.Type
                };

                collection.Add(model);
            }

            return collection;
        }

        public CreateProductSizeModel GetCreateSizeModel()
        {
            var types = this.DbContext
                .ProductTypes
                .Select(x => x.Type)
                .ToHashSet();

            var model = new CreateProductSizeModel
            {
                Types = types
            };

            return model;
        }

        public async Task CreateSizeAsync(CreateProductSizeModel model)
        {
            var values = model.Values
                .Split(new char[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            var productType = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(model.Type));

            foreach (var value in values)
            {
                var size = new ProductSize
                {
                    Value = value,
                    ProductTypeId = productType.Id
                };

                await this.DbContext
                    .ProductSizes.AddAsync(size);

                await this.DbContext
                    .SaveChangesAsync();
            }
        }

        public async Task DeleteSizeAsync(int id)
        {
            var size = await this.DbContext
                .ProductSizes
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            this.DbContext
                .ProductSizes
                .Remove(size);

            await this.DbContext
                .SaveChangesAsync();
        }

        public async Task EditSizeAsync(BaseProductSizeModel model)
        {
            if (model.Name != null)
            {
                var size = await this.DbContext
                .ProductSizes
                .FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

                size.Value = model.Name;

                await this.DbContext
                    .SaveChangesAsync();
            }
        }

        public async Task<BaseProductSizeModel> GetBaseSizeModelAsync(int id)
        {
            return await this.DbContext
                .ProductSizes
                .Select(x => new BaseProductSizeModel
                {
                    Id = x.Id,
                    Name = x.Value
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<ProductManageSizesModel> GetCurrentProductSizeDetails(int productId, string categoryName)
        {
            var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Name.Equals(categoryName));

            var allProductSizes = this.DbContext
                .ProductSizes
                .Where(x => x.ProductTypeId.Equals(category.ProductTypeId))
                .Select(x => x.Value)
                .ToHashSet();

            var productSpecifications = this.DbContext
                .ProductSpecifications
                .Where(x => x.ProductId.Equals(productId))
                .ToList();

            var dictionary = new Dictionary<string, uint>();

            foreach (var kvp in productSpecifications)
            {
                var sizeName = await this.DbContext
                    .ProductSizes
                    .FirstOrDefaultAsync(x => x.Id.Equals(kvp.ProductSizeId));

                var key = sizeName.Value;

                var value = kvp.Quantity;

                dictionary.Add(key, value);
            }

            var model = new ProductManageSizesModel
            {
                ProductId = productId,
                CurrentSizes = dictionary,
                AllProductSizes = allProductSizes,
                CategoryName = category.Name,
            };

            return model;
        }

        public async Task AddQuantityToProductAsync(int id, ProductManageSizesModel model)
        {
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var productSize = await this.DbContext
                .ProductSizes
                .FirstOrDefaultAsync(x => x.Value.Equals(model.ChoosenSize));

            var specification = await this.DbContext
                .ProductSpecifications
                .FirstOrDefaultAsync(x => x.ProductId.Equals(product.Id)
                    && x.ProductSizeId.Equals(productSize.Id));

            if (specification == null)
            {
                specification = new ProductSpecification
                {
                    ProductId = product.Id,
                    ProductSizeId = productSize.Id,
                    Quantity = model.InputQuantity
                };

                product.Specifications.Add(specification);
            }
            else
            {
                specification.Quantity += model.InputQuantity;
            }

            await this.DbContext
                .SaveChangesAsync();
        }

        public async Task RemoveQuantityFromProductAsync(int id, ProductManageSizesModel model)
        {
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var productSize = await this.DbContext
                .ProductSizes
                .FirstOrDefaultAsync(x => x.Value.Equals(model.ChoosenSize));

            var specification = await this.DbContext
                .ProductSpecifications
                .FirstOrDefaultAsync(x => x.ProductId.Equals(product.Id)
                    && x.ProductSizeId.Equals(productSize.Id));

            if (specification != null
                && model.InputQuantity <= specification.Quantity)
            {
                specification.Quantity -= model.InputQuantity;

                await this.DbContext
                    .SaveChangesAsync();
            }
        }
    }
}
