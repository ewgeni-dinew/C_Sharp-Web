using BabyBug.Common.ViewModels.Categories;
using BabyBug.Data.Models;
using BabyBug.Services.Categories.Contracts;
using BabyBugZone.Data;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BabyBug.Services
{
    public class CategoriesService : BaseCloudinaryService, ICategoriesService
    {
        private const string IMAGE_PATH = @"Categories";

        public CategoriesService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public ICollection<BaseCategoryModel> GetAllGarmentCategories()
        {
            var categoryList = this.DbContext
                .GarmentCategories
                .Select(x => new BaseCategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl
                })
                .ToList();            

            return categoryList;
        }

        public async Task CreateCategoryAsync(CreateCategoryModel model)
        {
            var file = model.Picture;

            //upload image to Cloudinary
            var uploadResult = this.UploadImageToCloudinary(file, IMAGE_PATH);

            //create category
            var category = new GarmentCategory
            {
                Name = model.Name,
                ImageUrl = BASE_PATH + uploadResult.PublicId,
                ImageId = uploadResult.PublicId
            };

            //insert category in Db
            await this.DbContext.GarmentCategories.AddAsync(category);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task<EditCategoryModel> GetEditCategoryModelAsync(int id)
        {
            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var model = new EditCategoryModel
            {
                Id = category.Id,
                Name = category.Name,
            };

            return model;
        }

        public async Task<DeleteCategoryModel> GetDeleteCategoryModelAsync(int id)
        {
            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var model = new DeleteCategoryModel
            {
                Name = category.Name,
                Id = category.Id
            };

            return model;
        }

        public async Task EditCategoryAsync(int id, EditCategoryModel model)
        {
            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (model.Name != null)
            {
                category.Name = model.Name;
            }
            if (model.Picture != null)
            {
                //remove image from Cloudinary
                this.RemoveImageFromCloudinary(category.ImageId);

                var uploadResult = this.UploadImageToCloudinary(model.Picture, IMAGE_PATH);

                category.ImageUrl = BASE_PATH + uploadResult.PublicId;
                category.ImageId = uploadResult.PublicId;
            }

            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            //remove image from Cloudinary
            this.RemoveImageFromCloudinary(category.ImageId);

            //remove category from Db
            this.DbContext
                .GarmentCategories
                .Remove(category);

            await this.DbContext.SaveChangesAsync();
        }
    }
}
