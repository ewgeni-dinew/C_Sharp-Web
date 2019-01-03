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
                ProductTypes = this.GetProductTypeNames(),
                CategoryNames = this.GetCategoryNames(type.Id),
                CategoryName = string.Empty,
                ProductType = type.Type,
                FilterModel = new FilterProductsModel
                {
                    Sizes = this.GetFilterSizesByType(type.Id)
                }
            };

            return model;
        }

        private HashSet<string> GetFilterSizesByType(int id)
        {
            return this.DbContext
                .ProductSizes
                .Where(x => x.ProductTypeId.Equals(id))
                .Select(x => x.Value)
                .ToHashSet();
        }

        public async Task<HomeCatalogModel> GetHomeModelByCategoryAsync(string categoryName)
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
                ProductTypes = this.GetProductTypeNames(),
                CategoryNames = this.GetCategoryNames(category.ProductTypeId),
                CategoryName = category.Name,
                ProductType = productType.Type,
                FilterModel = new FilterProductsModel()
            };

            return model;
        }

        public async Task<HomeCatalogModel> GetHomeModelByCriteriaAsync(FilterProductsModel model)
        {
            ICollection<BaseProductModel> productsTemp;

            var type = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(model.ProductType));

            if (model.CategoryName != null)
            {
                productsTemp = await this.GetProductsByCategoryAsync(model.CategoryName);
            }
            else //(model.ProductTypeName != null)
            {
                productsTemp = await this.GetProductsByTypeAsync(model.ProductType);
            }
            //else
            //{
            //    productsTemp = this.DbContext
            //        .Products
            //        .Select(x => new BaseProductModel
            //        {
            //            Id = x.Id,
            //            Name = x.Name,
            //            ImageUrl = x.ImageUrl,
            //            Price = x.Price
            //        })
            //        .ToHashSet();
            //}

            var products = await this.GetProductsByCriteriaAsync(productsTemp, model);

            var resultModel = new HomeCatalogModel
            {
                Products = products,
                ProductTypes = this.GetProductTypeNames(),
                CategoryNames = this.GetCategoryNames(type.Id),
                CategoryName = model.CategoryName,
                ProductType = model.ProductType,
                FilterModel = model
            };

            return resultModel;
        }

        private HashSet<string> GetProductTypeNames()
        {
            return this.DbContext
                .ProductTypes
                .Select(x => x.Type)
                .ToHashSet();
        }

        private HashSet<string> GetCategoryNames(int typeId)
        {
            return this.DbContext
                .ProductCategories
                .Where(x => x.ProductTypeId.Equals(typeId))
                .Select(x => x.Name)
                .ToHashSet();
        }

        private async Task<ICollection<BaseProductModel>> GetProductsByCriteriaAsync(ICollection<BaseProductModel> productsTemp, FilterProductsModel model)
        {
            ICollection<BaseProductModel> products;

            int startPrice = 0;
            int endPrice = 0;

            if (model.StartPrice != 0)
            {
                startPrice = model.StartPrice;
            }
            if (model.EndPrice != 0)
            {
                endPrice = model.EndPrice;
            }

            products = productsTemp
                .Where(x => x.Price >= startPrice && x.Price <= endPrice)
                .ToHashSet();

            if (model.Gender != null)
            {
                products = products
                    .Where(x => x.Gender.Equals(model.Gender))
                    .ToHashSet();
            }

            if (model.ChosenSizes.Any())
            {
                productsTemp = new HashSet<BaseProductModel>();

                foreach (var product in products)
                {
                    var productSizeIds = this.DbContext
                        .ProductSpecifications
                        .Where(x => x.ProductId.Equals(product.Id))
                        .Select(x => x.ProductSizeId)
                        .ToHashSet();

                    foreach (var sizeId in productSizeIds)
                    {
                        var sizeName = await this.DbContext
                            .ProductSizes
                            .Select(x => new
                            {
                                x.Id,
                                x.Value
                            })
                            .FirstOrDefaultAsync(x => x.Id.Equals(sizeId));

                        if (model.ChosenSizes.Contains(sizeName.Value))
                        {
                            productsTemp.Add(product);
                        }
                    }
                }
            }

            if (productsTemp.Any())
            {
                return productsTemp;
            }
            else
            {
                return products;
            }
        }

        private async Task<ICollection<BaseProductModel>> GetProductsByTypeAsync(string productTypeName)
        {
            var productType = await this.DbContext
                .ProductTypes
                .Select(x => new
                {
                    x.Type,
                    x.Id
                })
                .FirstOrDefaultAsync(x => x.Type.Equals(productTypeName));

            var products = this.DbContext
                .Products
                .Where(x => x.TypeId.Equals(productType.Id))
                .Select(x => new BaseProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price
                })
                .ToHashSet();

            return products;
        }

        private async Task<ICollection<BaseProductModel>> GetProductsByCategoryAsync(string categoryName)
        {
            var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Name.Equals(categoryName));

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

            return products;
        }
    }
}
