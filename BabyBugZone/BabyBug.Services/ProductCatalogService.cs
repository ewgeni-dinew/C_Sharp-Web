using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.ProductCatalog;
using BabyBug.Data.Models;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;

namespace BabyBug.Services
{
    public class ProductCatalogService : BaseDbService, IProductCatalogService
    {
        private const int CURRENT_START_PAGE = 1;
        private const int PRODUCTS_PER_PAGE_COUNT = 1;

        //private const int PAGE_ITEMS_MAXCOUNT = 3;

        public ProductCatalogService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task<HomeCatalogModel> GetHomeViewModelAsync()
        {
            return await this.GetHomeModelByTypeAsync("Garment");
        }

        public async Task<HomeCatalogModel> GetHomeModelByTypeAsync(string typeName)
        {
            var type = await this.GetProductTypeByNameAsync(typeName);

            var products = await this.GetProductsByTypeAsync(typeName);

            var model = new HomeCatalogModel()
            {
                AllProducts = products,
                ProductTypes = this.GetProductTypeNames(),
                CategoryNames = this.GetCategoryNames(type.Id),
                CategoryName = string.Empty,
                ProductType = type.Type,
                FilterModel = new FilterProductsModel
                {
                    Sizes = this.GetFilterSizesByType(type.Id),
                    Gender = string.Empty
                },
                PaginationModel = this.GetPaginationModel(products, CURRENT_START_PAGE)
            };

            return model;
        }

        public async Task<HomeCatalogModel> SetPaginationModelAsync(int pageIndex, HomeCatalogModel model)
        {
            var homeModel = await this.GetHomeModelByCriteriaAsync(model);

            var paginationModel = this.GetPaginationModel(homeModel.AllProducts, pageIndex);

            homeModel.PaginationModel = paginationModel;

            return homeModel;
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

            var products = await this.GetProductsByCategoryAsync(categoryName);

            var model = new HomeCatalogModel()
            {
                AllProducts = products,
                ProductTypes = this.GetProductTypeNames(),
                CategoryNames = this.GetCategoryNames(category.ProductTypeId),
                CategoryName = category.Name,
                ProductType = productType.Type,
                FilterModel = new FilterProductsModel()
                {
                    Sizes = this.GetFilterSizesByType(productType.Id),
                    Gender = string.Empty
                },
                PaginationModel = this.GetPaginationModel(products, CURRENT_START_PAGE)
            };

            return model;
        }

        public async Task<HomeCatalogModel> GetHomeModelByCriteriaAsync(HomeCatalogModel model)
        {
            IEnumerable<BaseProductModel> productsTemp;
            IEnumerable<BaseProductModel> products;
            var gender = model.FilterModel.Gender;

            var type = await this.GetProductTypeByNameAsync(model.ProductType);

            if (model.CategoryName != null)
            {
                productsTemp = await this.GetProductsByCategoryAsync(model.CategoryName);
            }
            else
            {
                productsTemp = await this.GetProductsByTypeAsync(model.ProductType);
            }

            if (model.FilterModel.GetType().GetProperties().Any(c => c.GetValue(model.FilterModel) != null))
            {
                products = await this.GetProductsByCriteriaAsync(productsTemp, model.FilterModel);
            }
            else
            {
                products = productsTemp;
                gender = string.Empty;
            }

            var resultModel = new HomeCatalogModel
            {
                AllProducts = products,
                ProductTypes = this.GetProductTypeNames(),
                CategoryNames = this.GetCategoryNames(type.Id),
                CategoryName = model.CategoryName,
                ProductType = model.ProductType,
                FilterModel = new FilterProductsModel
                {
                    Gender = gender,
                    Sizes = this.GetFilterSizesByType(type.Id),
                },
                PaginationModel = this.GetPaginationModel(products, CURRENT_START_PAGE)
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

        private async Task<IEnumerable<BaseProductModel>> GetProductsByCriteriaAsync(
            IEnumerable<BaseProductModel> productsTemp, FilterProductsModel model)
        {
            IEnumerable<BaseProductModel> products;

            int startPrice = 0;
            int endPrice = 0;

            if (model.PriceRange != null)
            {
                var priceRange = model.PriceRange
                            .Split(new char[] { ' ', '-', '$' }, StringSplitOptions.RemoveEmptyEntries);

                startPrice = int.Parse(priceRange[0]);
                endPrice = int.Parse(priceRange[1]);
            }

            if (model.Gender != null)
            {
                products = productsTemp
                        .Where(x => x.Price >= startPrice && x.Price <= endPrice && x.Gender.Equals(model.Gender))
                        .ToHashSet();
            }
            else
            {
                products = productsTemp
                        .Where(x => x.Price >= startPrice && x.Price <= endPrice)
                        .ToHashSet();
            }

            //if (model.ChosenSizes.Any())
            //{
            //    productsTemp = new HashSet<BaseProductModel>();

            //    foreach (var product in products)
            //    {
            //        var productSizeIds = this.DbContext
            //            .ProductSpecifications
            //            .Where(x => x.ProductId.Equals(product.Id))
            //            .Select(x => x.ProductSizeId)
            //            .ToHashSet();

            //        foreach (var sizeId in productSizeIds)
            //        {
            //            var sizeName = await this.DbContext
            //                .ProductSizes
            //                .Select(x => new
            //                {
            //                    x.Id,
            //                    x.Value
            //                })
            //                .FirstOrDefaultAsync(x => x.Id.Equals(sizeId));

            //            if (model.ChosenSizes.Contains(sizeName.Value))
            //            {
            //                productsTemp.Add(product);
            //            }
            //        }
            //    }
            //}

            //if (productsTemp.Any())
            //{
            //    return productsTemp;
            //}
            await this.DbContext.SaveChangesAsync();

            return products;
        }

        private async Task<IEnumerable<BaseProductModel>> GetProductsByTypeAsync(string productTypeName)
        {
            var productType = await this.GetProductTypeByNameAsync(productTypeName);

            var products = this.DbContext
                .Products
                .Where(x => x.TypeId.Equals(productType.Id))
                .Select(x => new BaseProductModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl,
                    Price = x.Price,
                    Gender = x.Gender.ToString(),
                })
                .ToHashSet();

            return products;
        }

        private async Task<ProductType> GetProductTypeByNameAsync(string productTypeName)
        {
            return await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(productTypeName));
        }

        private async Task<IEnumerable<BaseProductModel>> GetProductsByCategoryAsync(string categoryName)
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
                    Price = x.Price,
                    Gender = x.Gender.ToString(),
                })
                .ToHashSet();

            return products;
        }

        private PaginationModel GetPaginationModel(IEnumerable<BaseProductModel> products, int pageIndex)
        {
            var model = new PaginationModel
            {
                CurrentPage = pageIndex,
                AllPages = this.GetAllPagesCount(products),
                DisplayProducts = this.GetDisplayProducts(products, pageIndex)
            };

            return model;
        }

        private IEnumerable<BaseProductModel> GetDisplayProducts(IEnumerable<BaseProductModel> products, int pageIndex)
        {
            return products
                .Skip((pageIndex - 1) * PRODUCTS_PER_PAGE_COUNT)
                .Take(PRODUCTS_PER_PAGE_COUNT)
                .ToHashSet();
        }

        private int GetAllPagesCount(IEnumerable<BaseProductModel> products)
        {
            return Math.Abs(products.Count() / PRODUCTS_PER_PAGE_COUNT);
        }

        private HashSet<string> GetFilterSizesByType(int id)
        {
            var list = this.DbContext
                .ProductSizes
                .Where(x => x.ProductTypeId.Equals(id))
                .Select(x => x.Value)
                .ToHashSet();

            return list;
        }

    }
}
