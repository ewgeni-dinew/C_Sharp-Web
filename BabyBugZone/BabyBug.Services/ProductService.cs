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
    public class ProductService : BaseCloudinaryService, IProductService
    {
        private const string IMAGE_PATH = @"Products";

        public ProductService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public CreateProductModel GetProductCreateModel()
        {
            var categories = this.DbContext
                .ProductCategories
                .Select(x => x.Name)
                .ToHashSet();

            var model = new CreateProductModel
            {
                CategoryNames = categories,
            };

            return model;
        }

        public async Task CreateProductAsync(CreateProductModel model)
        {
            //get category id
            var categoryId = this.DbContext
                .ProductCategories
                .FirstOrDefault(x => x.Name.Equals(model.CategoryName))
                .Id;

            var file = model.Picture;

            //upload image to Cloudinary
            var uploadResult = this.UploadImageToCloudinary(file, IMAGE_PATH);

            var product = new Product
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
            await this.DbContext
                .Products
                .AddAsync(product);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task<ProductDetailsModel> GetDetailsModelAsync(int id)
        {
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var productSizes = this.DbContext
                .ProductSpecifications
                .Where(x => x.ProductId.Equals(id))
                .ToList();

            var availableSizes = new HashSet<string>();

            foreach (var size in productSizes)
            {
                var sizeName = await this.DbContext
                    .ProductSizes
                    .FirstOrDefaultAsync(x => x.Id.Equals(size.ProductSizeId));

                availableSizes.Add(sizeName.Value);
            }

            var model = new ProductDetailsModel
            {
                AvailableSizes = availableSizes,
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Gender = product.Gender,
                CreatedOn = product.CreatedOn.ToString("dd-MM-yyyy"),
                ImageUrl = product.ImageUrl
            };

            return model;
        }

        public async Task<DeleteProductModel> GetDeleteModelAsync(int id)
        {
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var model = new DeleteProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Gender = product.Gender,
                CreatedOn = product.CreatedOn.ToString("dd-MM-yyyy")
            };

            return model;
        }

        public async Task<EditProductModel> GetEditModelAsync(int id)
        {
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var categories = this.DbContext
                .ProductCategories
                .Select(x => x.Name)
                .ToHashSet();

            var productCategory = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Id.Equals(product.CategoryId));

            var model = new EditProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Gender = product.Gender,
                CategoryNames = categories,
                CategoryName = productCategory.Name
            };

            return model;
        }

        public async Task DeleteProductAsync(int id)
        {
            //get garment
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            //remove image from Cloudinary
            this.RemoveImageFromCloudinary(product.ImageId);

            this.DbContext
                .Products
                .Remove(product);

            await this.DbContext
                .SaveChangesAsync();
        }

        public async Task EditProductAsync(int id, EditProductModel model)
        {
            var product = await this.DbContext
                .Products
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (model.Name != null)
            {
                product.Name = model.Name;
            }
            if (model.CategoryName != null)
            {
                var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Name.Equals(model.CategoryName));

                product.CategoryId = category.Id;
            }
            if (model.Description != null)
            {
                product.Description = model.Description;
            }
            if (model.Price > 0)
            {
                product.Price = model.Price;
            }
            if (model.Picture != null)
            {
                //update picture

                //remove old image from Cloudinary
                this.RemoveImageFromCloudinary(product.ImageId);

                //upload new image
                var uploadResult = this.UploadImageToCloudinary(model.Picture, IMAGE_PATH);

                product.ImageUrl = BASE_PATH + uploadResult.PublicId;
                product.ImageId = uploadResult.PublicId;
            }

            product.Gender = model.Gender;

            await this.DbContext
                .SaveChangesAsync();
        }
    }
}
