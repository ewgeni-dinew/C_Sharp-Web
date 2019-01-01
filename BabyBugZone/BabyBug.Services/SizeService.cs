﻿using BabyBug.Common.ViewModels.ProductSize;
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
    public class SizeService : BaseCloudinaryService, ISizeService
    {
        public SizeService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public ICollection<BaseProductSizeModel> GetAllGarmentSizes()
        {
            var garmentsSizes = this.DbContext
                .ProductSizes
                .ToHashSet();

            var collection = new HashSet<BaseProductSizeModel>();

            foreach (var size in garmentsSizes)
            {
                var model = new BaseProductSizeModel
                {
                    Id = size.Id,
                    Name = size.Value
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

        public async Task<ProductManageSizesModel> GetCurrentGarmentSizeDetails(int id)
        {
            //AllGarmentSizes
            var allGarmentSizes = this.DbContext
                .ProductSizes
                .Select(x => x.Value)
                .ToHashSet();

            var garmentSpecifications = this.DbContext
                .ProductSpecifications
                .Where(x => x.ProductId.Equals(id))
                .ToList();

            var dictionary = new Dictionary<string, uint>();

            foreach (var kvp in garmentSpecifications)
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
                GarmentId = id,
                CurrentSizes = dictionary,
                AllGarmentSizes = allGarmentSizes,
            };

            return model;
        }

        public async Task AddQuantityToGarmentAsync(int id, ProductManageSizesModel model)
        {
            var garment = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var garmentSize = await this.DbContext
                .ProductSizes
                .FirstOrDefaultAsync(x => x.Value.Equals(model.ChoosenSize));

            var specification = await this.DbContext
                .ProductSpecifications
                .FirstOrDefaultAsync(x => x.ProductId.Equals(garment.Id)
                    && x.ProductSizeId.Equals(garmentSize.Id));

            if (specification == null)
            {
                specification = new ProductSpecification
                {
                    ProductId = garment.Id,
                    ProductSizeId = garmentSize.Id,
                    Quantity = model.InputQuantity
                };

                garment.Specifications.Add(specification);
            }
            else
            {
                specification.Quantity += model.InputQuantity;
            }
            await this.DbContext.SaveChangesAsync();
        }

        public async Task RemoveQuantityFromGarmentAsync(int id, ProductManageSizesModel model)
        {
            var garment = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var garmentSize = await this.DbContext
                .ProductSizes
                .FirstOrDefaultAsync(x => x.Value.Equals(model.ChoosenSize));

            var specification = await this.DbContext
                .ProductSpecifications
                .FirstOrDefaultAsync(x => x.ProductId.Equals(garment.Id)
                    && x.ProductSizeId.Equals(garmentSize.Id));

            if (specification != null
                && model.InputQuantity <= specification.Quantity)
            {
                specification.Quantity -= model.InputQuantity;

                await this.DbContext.SaveChangesAsync();
            }
        }
    }
}