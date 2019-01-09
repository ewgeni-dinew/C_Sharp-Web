using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Products;
using BabyBug.Common.ViewModels.Reviews;
using BabyBug.Data.Models;
using BabyBug.Data.Models.Enums;
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
        private const int DISPLAY_REVIEW_COUNT = 3;
        private const int PRODUCT_SHORT_DESCRIPTION_COUNT = 90;

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

            var productTypes = this.DbContext
                .ProductTypes
                .Select(x => x.Type)
                .ToHashSet();

            var model = new CreateProductModel
            {
                CategoryNames = categories,
                ProductTypes = productTypes
            };

            return model;
        }

        public async Task CreateProductAsync(CreateProductModel model)
        {
            if (!this.IsValidImageFile(model.Picture))
            {
                throw new ArgumentException("Invalid file type!");
            }

            //get category id
            var category = await this.DbContext
                .ProductCategories
                .FirstOrDefaultAsync(x => x.Name.Equals(model.CategoryName));

            var productType = await this.DbContext
                .ProductTypes
                .FirstOrDefaultAsync(x => x.Id.Equals(category.ProductTypeId));

            if (!productType.Type.Equals(model.ProductType))
            {
                throw new ArgumentException();
            }

            var file = model.Picture;

            //upload image to Cloudinary
            var uploadResult = this.UploadImageToCloudinary(file, IMAGE_PATH);

            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Gender = model.Gender,
                CategoryId = category.Id,
                ImageId = uploadResult.PublicId,
                ImageUrl = BASE_PATH + uploadResult.PublicId,
                TypeId = category.ProductTypeId
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
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsAvailable == true);

            if (product == null)
            {
                throw new ArgumentException("Product is unavailable at the moment.");
            }

            var category = await this.DbContext
                .ProductCategories
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                })
                .FirstOrDefaultAsync(x => x.Id.Equals(product.CategoryId));

            //get avrg(rating) and all product revies ->
            var reviews = new HashSet<BaseDisplayProductReviewModel>();

            var ratings = new List<int>();

            var user_reviews = this.DbContext
                .UserReviews
                .Where(x => x.ProductId.Equals(id))
                .ToHashSet();

            foreach (var ur in user_reviews)
            {
                var review = await this.DbContext
                    .Reviews
                    .FirstOrDefaultAsync(x => x.Id.Equals(ur.ReviewId) && x.Status == ReviewStatus.Approved);

                if (review != null)
                {
                    reviews.Add(new BaseDisplayProductReviewModel
                    {
                        Author = review.UserName,
                        Content = review.Content,
                        CreatedOn = review.CreatedOn.ToString("dd/MM/yyyy"),
                        Rating = review.Rating
                    });
                    ratings.Add(review.Rating);
                }
            }
            if (!ratings.Any())
            {
                ratings.Add(0);
            }


            //get product sizes
            var productSizes = this.DbContext
                .ProductSpecifications
                .Where(x => x.ProductId.Equals(id) && x.Quantity > 0)
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
                TypeId = (int)product.TypeId,
                CategoryName = category.Name,
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.Description.Substring(0, PRODUCT_SHORT_DESCRIPTION_COUNT),
                Description = product.Description,
                Price = product.Price,
                Gender = product.Gender,
                CreatedOn = product.CreatedOn.ToString("dd-MM-yyyy"),
                ImageUrl = product.ImageUrl,
                ProductReviews = reviews.Take(DISPLAY_REVIEW_COUNT).ToHashSet(),
                Rating = (int)Math.Round(ratings.Average()),
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
                //check if valid picture
                if (!this.IsValidImageFile(model.Picture))
                {
                    throw new ArgumentException("Invalid file type!");
                }

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

        public async Task<ICollection<BaseProductModel>> GetOutOfStockProductsModelAsync()
        {
            var model = new HashSet<BaseProductModel>();

            foreach (var product in this.DbContext.Products.Where(x => x.IsAvailable == false))
            {
                var type = await this.DbContext.ProductTypes.FirstOrDefaultAsync(x => x.Id.Equals(product.TypeId));

                model.Add(new BaseProductModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Type = type.Type,
                    TypeId = (int)product.TypeId,
                    CreatedOn = product.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                });
            }
            return model;
        }

        private Dictionary<int, string> GetProductTypes()
        {
            var result = this.DbContext.ProductTypes.ToDictionary(x => x.Id, x => x.Type);

            //var result = await this.DbContext.ProductTypes.FirstOrDefaultAsync(x => x.Id.Equals(typeId));

            return result;
        }
    }
}
