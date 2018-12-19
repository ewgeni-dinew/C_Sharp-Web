using BabyBug.Common.ViewModels.Garments;
using BabyBug.Data.Models;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class GarmentsService : IGarmentsService
    {
        public GarmentsService(BabyBugDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public BabyBugDbContext DbContext { get; set; }

        public CreateGarmentModel GetGarmentCreateModel()
        {
            var categories = this.DbContext
                .GarmentCategories
                .Select(x => x.Name)
                .ToHashSet();

            var model = new CreateGarmentModel
            {
                CategoryNames = categories,
            };

            return model;
        }

        public async Task CreateGarmentAsync(CreateGarmentModel model)
        {
            var categoryId = this.DbContext
                .GarmentCategories
                .FirstOrDefault(x => x.Name.Equals(model.CategoryName))
                .Id;

            var garment = new Garment
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Gender = model.Gender,
                CategoryId = categoryId
            };

            await this.DbContext.Garments.AddAsync(garment);
            await this.DbContext.SaveChangesAsync();
        }

        public async Task<GarmentDetailsModel> GetDetailsModelAsync(int id)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var categoryNames = this.DbContext
                .GarmentCategories
                .Select(x => x.Name)
                .ToHashSet();

            var model = new GarmentDetailsModel
            {
                Id = garment.Id,
                Name = garment.Name,
                Description = garment.Description,
                CategoryNames=categoryNames,
                Price = garment.Price,
                Gender = garment.Gender,
                CreatedOn = garment.CreatedOn.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task<DeleteGarmentModel> GetDeleteModelAsync(int id)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var model = new DeleteGarmentModel
            {
                Id = garment.Id,
                Name = garment.Name,
                Description = garment.Description,
                Price = garment.Price,
                Gender = garment.Gender,
                CreatedOn = garment.CreatedOn.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task DeleteGarmentAsync(int id)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            this.DbContext
                .Garments
                .Remove(garment);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task EditGarmentAsync(GarmentDetailsModel model)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(model.Id));

            var categoryId = this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Name.Equals(model.CategoryName))
                .Id;

            garment.Name = model.Name;
            garment.CategoryId = categoryId;
            garment.Description = model.Description;
            garment.Gender = model.Gender;
            garment.Price = model.Price;

            await this.DbContext.SaveChangesAsync();
        }
    }
}
