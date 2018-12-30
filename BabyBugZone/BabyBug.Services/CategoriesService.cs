using BabyBug.Common.ViewModels.Categories;
using BabyBug.Data.Models;
using BabyBug.Data.Models.Categories;
using BabyBug.Data.Models.Contracts;
using BabyBug.Data.Models.Enums;
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

        public AllCategoriesModel GetAllGarmentCategories()
        {
            var garmentList = this.DbContext
                .GarmentCategories
                .Select(x => new BaseCategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            var shoeList = this.DbContext
                .ShoeCategories
                .Select(x => new BaseCategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            var accessoryList = this.DbContext
                .AccessoryCategories
                .Select(x => new BaseCategoryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ImageUrl = x.ImageUrl
                })
                .ToList();

            var model = new AllCategoriesModel
            {
                GarmentCategories = garmentList,
                ShoeCategories = shoeList,
                AccessoryCategories = accessoryList
            };

            return model;
        }

        public async Task CreateCategoryAsync(CreateCategoryModel model)
        {
            var file = model.Picture;

            //upload image to Cloudinary
            var uploadResult = this.UploadImageToCloudinary(file, IMAGE_PATH);

            //create category
            ICategory category;
            var categoryType = Enum.Parse<CategoryType>(model.CategoryType);

            switch (categoryType)
            {
                case CategoryType.Garment:
                    category = new GarmentCategory();
                    await this.DbContext.GarmentCategories.AddAsync((GarmentCategory)category);
                    break;
                case CategoryType.Shoe:
                    category = new ShoeCategory();
                    await this.DbContext.ShoeCategories.AddAsync((ShoeCategory)category);

                    break;
                case CategoryType.Accessory:
                    category = new AccessoryCategory();
                    await this.DbContext.AccessoryCategories.AddAsync((AccessoryCategory)category);

                    break;
                default: throw new NotImplementedException();
            }

            //insert values
            category.Name = model.Name;
            category.ImageUrl = BASE_PATH + uploadResult.PublicId;
            category.ImageId = uploadResult.PublicId;

            //insert category in Db

            await this.DbContext
                .SaveChangesAsync();
        }

        public async Task<EditCategoryModel> GetEditGarmentCatModelAsync(int id)
        {
            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return this.GetEditCategoryModel(category);
        }

        public async Task<EditCategoryModel> GetEditShoeCatModelAsync(int id)
        {
            var category = await this.DbContext
                .ShoeCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return this.GetEditCategoryModel(category);
        }

        public async Task<EditCategoryModel> GetEditAccessoryCatModelAsync(int id)
        {
            var category = await this.DbContext
                .AccessoryCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            return this.GetEditCategoryModel(category);
        }

        public async Task EditGarmentCategoryAsync(int id, EditCategoryModel model)
        {
            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            await this.EditCategoryAsync(category, model);
        }

        public async Task EditShoeCategoryAsync(int id, EditCategoryModel model)
        {
            var category = await this.DbContext
                .ShoeCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            await this.EditCategoryAsync(category, model);
        }

        public async Task EditAccessoryCategoryAsync(int id, EditCategoryModel model)
        {
            var category = await this.DbContext
                .AccessoryCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            await this.EditCategoryAsync(category, model);
        }

        public async Task DeleteGarmentCategoryAsync(int id)
        {
            var category = await this.DbContext
                .GarmentCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            //remove image from Cloudinary
            this.RemoveImageFromCloudinary(category.ImageId);

            //remove product images from Cloudinary
            foreach (var product in category.Garments)
            {
                this.RemoveImageFromCloudinary(product.ImageId);
            }

            //remove category from Db
            this.DbContext
                .GarmentCategories
                .Remove(category);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteShoeCategoryAsync(int id)
        {
            var category = await this.DbContext
                .ShoeCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            //remove image from Cloudinary
            this.RemoveImageFromCloudinary(category.ImageId);

            //remove product images from Cloudinary
            foreach (var product in category.Shoes)
            {
                this.RemoveImageFromCloudinary(product.ImageId);
            }

            //remove category from Db
            this.DbContext
                .ShoeCategories
                .Remove(category);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteAccessoryCategoryAsync(int id)
        {
            var category = await this.DbContext
                .AccessoryCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            //remove image from Cloudinary
            this.RemoveImageFromCloudinary(category.ImageId);

            //remove product images from Cloudinary
            foreach (var product in category.Accessories)
            {
                this.RemoveImageFromCloudinary(product.ImageId);
            }

            //remove category from Db
            this.DbContext
                .AccessoryCategories
                .Remove(category);

            await this.DbContext.SaveChangesAsync();
        }

        private EditCategoryModel GetEditCategoryModel(ICategory category)
        {
            var model = new EditCategoryModel
            {
                Id = category.Id,
                Name = category.Name,
            };

            return model;
        }

        private async Task EditCategoryAsync(ICategory category, EditCategoryModel model)
        {
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

            await this.DbContext
                .SaveChangesAsync();
        }

    }
}
