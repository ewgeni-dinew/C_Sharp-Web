using BabyBug.Common.ViewModels.Garments;
using BabyBug.Data.Models;
using BabyBug.Data.Models.Products;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class GarmentsService : BaseCloudinaryService, IGarmentsService
    {
        private const string IMAGE_PATH = @"/Products/Garments";

        public GarmentsService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

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
            //get category id
            var categoryId = this.DbContext
                .GarmentCategories
                .FirstOrDefault(x => x.Name.Equals(model.CategoryName))
                .Id;

            var file = model.Picture;

            //upload image to Cloudinary
            var uploadResult = this.UploadImageToCloudinary(file, IMAGE_PATH);

            var garment = new Garment
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Gender = model.Gender,
                CategoryId = categoryId,
                ImageId = uploadResult.PublicId,
                ImageUrl = BASE_PATH + uploadResult.PublicId,
            };

            //add garment to DB
            await this.DbContext.Garments.AddAsync(garment);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task<GarmentDetailsModel> GetDetailsModelAsync(int id)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var garmentSizes = this.DbContext
                .GarmentSpecifications
                .Where(x => x.ProductId.Equals(id))
                .ToList();

            var availableSizes = new HashSet<string>();

            foreach (var size in garmentSizes)
            {
                var sizeName = await this.DbContext
                    .ProductSizes
                    .FirstOrDefaultAsync(x => x.Id.Equals(size.ProductSizeId));

                availableSizes.Add(sizeName.Value);
            }

            var model = new GarmentDetailsModel
            {
                AvailableSizes = availableSizes,
                Id = garment.Id,
                Name = garment.Name,
                Description = garment.Description,
                Price = garment.Price,
                Gender = garment.Gender,
                CreatedOn = garment.CreatedOn.ToString("dd-MM-yyyy"),
                ImageUrl = garment.ImageUrl
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

        public async Task<EditGarmentModel> GetEditModelAsync(int id)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var categories = this.DbContext
                .GarmentCategories
                .Select(x => x.Name)
                .ToHashSet();

            var garmentCategory = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(garment.CategoryId));

            var model = new EditGarmentModel
            {
                Id = garment.Id,
                Name = garment.Name,
                Description = garment.Description,
                Price = garment.Price,
                Gender = garment.Gender,
                CategoryNames = categories,
                CategoryName = garmentCategory.Name
            };

            return model;
        }

        public async Task DeleteGarmentAsync(int id)
        {
            //get garment
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            //remove image from Cloudinary
            this.RemoveImageFromCloudinary(garment.ImageId);

            this.DbContext
                .Garments
                .Remove(garment);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task EditGarmentAsync(int id, EditGarmentModel model)
        {
            var garment = await this.DbContext
                .Garments
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (model.Name != null)
            {
                garment.Name = model.Name;
            }
            if (model.CategoryName != null)
            {
                var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Name.Equals(model.CategoryName));

                garment.CategoryId = category.Id;
            }
            if (model.Description != null)
            {
                garment.Description = model.Description;
            }
            if (model.Price > 0)
            {
                garment.Price = model.Price;
            }
            if (model.Picture != null)
            {
                //update picture

                //remove old image from Cloudinary
                this.RemoveImageFromCloudinary(garment.ImageId);

                //upload new image
                var uploadResult = this.UploadImageToCloudinary(model.Picture, IMAGE_PATH);

                garment.ImageUrl = BASE_PATH + uploadResult.PublicId;
                garment.ImageId = uploadResult.PublicId;
            }

            garment.Gender = model.Gender;

            await this.DbContext.SaveChangesAsync();
        }
    }
}
