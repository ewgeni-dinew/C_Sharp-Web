using BabyBug.Common.ViewModels.GarmentSize;
using BabyBug.Data.Models;
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
    public class GarmentSizeService : BaseCloudinaryService, IGarmentSizeService
    {
        public GarmentSizeService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public ICollection<BaseGarmentSizeModel> GetAllGarmentSizes()
        {
            var garmentsSizes = this.DbContext
                .GarmentSizes
                .ToHashSet();

            var collection = new HashSet<BaseGarmentSizeModel>();

            foreach (var size in garmentsSizes)
            {
                var model = new BaseGarmentSizeModel
                {
                    Id = size.Id,
                    Name = size.Value
                };

                collection.Add(model);
            }

            return collection;
        }

        public async Task CreateSizeAsync(CreateGarmentSizeModel model)
        {
            var values = model.Values
                .Split(new char[] { ' ', ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            foreach (var value in values)
            {
                var size = new GarmentSize
                {
                    Value = value
                };

                await this.DbContext.GarmentSizes.AddAsync(size);

                await this.DbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteSizeAsync(int id)
        {
            var size = await this.DbContext
                .GarmentSizes
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            this.DbContext.GarmentSizes.Remove(size);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task EditSizeAsync(BaseGarmentSizeModel model)
        {
            if (model.Name != null)
            {
                var size = await this.DbContext
                .GarmentSizes
                .FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

                size.Value = model.Name;

                await this.DbContext.SaveChangesAsync();
            }
        }

        public async Task<BaseGarmentSizeModel> GetBaseSizeModelAsync(int id)
        {
            return await this.DbContext
                .GarmentSizes
                .Select(x => new BaseGarmentSizeModel
                {
                    Id = x.Id,
                    Name = x.Value
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<GarmentManageSizesModel> GetCurrentGarmentSizeDetails(int id)
        {
            //AllGarmentSizes
            var allGarmentSizes = this.DbContext
                .GarmentSizes
                .Select(x => x.Value)
                .ToHashSet();

            var garmentSpecifications = this.DbContext
                .GarmentSpecifications
                .Where(x => x.GarmentId.Equals(id))
                .ToList();

            var dictionary = new Dictionary<string, uint>();

            foreach (var kvp in garmentSpecifications)
            {
                var sizeName = await this.DbContext
                    .GarmentSizes
                    .FirstOrDefaultAsync(x => x.Id.Equals(kvp.GarmentSizeId));

                var key = sizeName.Value;

                var value = kvp.Quantity;

                dictionary.Add(key, value);
            }

            var model = new GarmentManageSizesModel
            {
                GarmentId = id,
                CurrentSizes = dictionary,
                AllGarmentSizes = allGarmentSizes,
            };

            return model;
        }

        public async Task AddQuantityToGarmentAsync(int id, GarmentManageSizesModel model)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var garmentSize = await this.DbContext
                .GarmentSizes
                .FirstOrDefaultAsync(x => x.Value.Equals(model.ChoosenSize));

            var specification = await this.DbContext
                .GarmentSpecifications
                .FirstOrDefaultAsync(x => x.GarmentId.Equals(garment.Id)
                    && x.GarmentSizeId.Equals(garmentSize.Id));

            if (specification == null)
            {
                specification = new GarmentSpecification
                {
                    GarmentId = garment.Id,
                    GarmentSizeId = garmentSize.Id,
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

        public async Task RemoveQuantityFromGarmentAsync(int id, GarmentManageSizesModel model)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var garmentSize = await this.DbContext
                .GarmentSizes
                .FirstOrDefaultAsync(x => x.Value.Equals(model.ChoosenSize));

            var specification = await this.DbContext
                .GarmentSpecifications
                .FirstOrDefaultAsync(x => x.GarmentId.Equals(garment.Id)
                    && x.GarmentSizeId.Equals(garmentSize.Id));

            if (specification != null
                && model.InputQuantity <= specification.Quantity)
            {
                specification.Quantity -= model.InputQuantity;

                await this.DbContext.SaveChangesAsync();
            }
        }
    }
}
