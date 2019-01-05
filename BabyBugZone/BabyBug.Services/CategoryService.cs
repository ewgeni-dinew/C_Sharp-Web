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
    public class CategoryService : BaseCloudinaryService, ICategoryService
    {
        private const string IMAGE_PATH = @"Categories";

        public CategoryService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public AllCategoriesModel GetAllProductCategories()
        {
            var model = new AllCategoriesModel()
            {
                AllCategories = new HashSet<CategoryIndexModel>(),
            };

            var productTypes = this.DbContext
                .ProductTypes
                .ToHashSet();

            foreach (var type in productTypes)
            {
                var categories = this.DbContext
                    .ProductCategories
                    .Where(x => x.ProductTypeId.Equals(type.Id))
                    .ToHashSet();

                foreach (var category in categories)
                {
                    var baseModel = new BaseCategoryModel()
                    {
                        Name = category.Name,
                        Id = category.Id,
                        ImageUrl = category.ImageUrl
                    };

                    if (!model.AllCategories.Any(x => x.TypeName.Equals(type.Type)))
                    {
                        var allCategoriesModel = new CategoryIndexModel
                        {
                            TypeName = type.Type,
                            SubCategories = new HashSet<BaseCategoryModel>(),
                        };

                        allCategoriesModel.SubCategories.Add(baseModel);

                        model.AllCategories.Add(allCategoriesModel);
                    }
                    else
                    {
                        model.AllCategories
                            .FirstOrDefault(x => x.TypeName.Equals(type.Type))
                            .SubCategories
                            .Add(baseModel);
                    }
                }
            }

            return model;
        }

        public CreateCategoryModel GetCreateCategoryModel()
        {
            var types = this.DbContext
                .ProductTypes
                .Select(x => x.Type)
                .ToHashSet();

            var model = new CreateCategoryModel
            {
                CategoryTypes = types
            };

            return model;
        }

        public async Task CreateCategoryAsync(CreateCategoryModel model)
        {
            var file = model.Picture;

            //upload image to Cloudinary
            var uploadResult = this.UploadImageToCloudinary(file, IMAGE_PATH);

            //create category
            var categoryType = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(model.CategoryType));

            var category = new ProductCategory()
            {
                Name = model.Name,
                ImageUrl = BASE_PATH + uploadResult.PublicId,
                ImageId = uploadResult.PublicId,
                ProductTypeId = categoryType.Id
            };

            //insert category in Db
            await this.DbContext
                .ProductCategories
                .AddAsync(category);

            await this.DbContext
                .SaveChangesAsync();
        }

        public async Task<EditCategoryModel> GetEditCategoryModelAsync(int id)
        {
            var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var types = this.DbContext
                .ProductTypes
                .Select(x => x.Type)
                .ToHashSet();

            var categoryType = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Id.Equals(category.ProductTypeId));

            var model = new EditCategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                CategoryType = categoryType.Type,
                CategoryTypes = types
            };

            return model;
        }

        public async Task EditCategoryAsync(int id, EditCategoryModel model)
        {
            var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var categoryType = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Id.Equals(category.ProductTypeId));

            var modelCategory = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Type.Equals(model.CategoryType));

            if (model.Name != category.Name)
            {
                category.Name = model.Name;
            }
            if (modelCategory.Type != categoryType.Type)
            {
                category.ProductTypeId = modelCategory.Id;
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

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            //remove image from Cloudinary
            this.RemoveImageFromCloudinary(category.ImageId);

            //remove product images from Cloudinary
            foreach (var product in category.Products)
            {
                this.RemoveImageFromCloudinary(product.ImageId);
            }

            //remove category from Db
            this.DbContext
                .ProductCategories
                .Remove(category);

            await this.DbContext
                .SaveChangesAsync();
        }
    }
}
