using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Reviews;
using BabyBug.Data.Models;
using BabyBug.Data.Models.Enums;
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
    public class ReviewService : BaseDbService, IReviewService
    {
        public ReviewService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task<ICollection<AdminReviewBaseModel>> GetAllCreatedReviewsModelAsync()
        {
            var model = new HashSet<AdminReviewBaseModel>();

            var reviews = this.DbContext
                .Reviews
                .Where(x => x.Status.Equals(ReviewStatus.Created))
                .ToHashSet();

            foreach (var review in reviews)
            {
                var user_review = await this.DbContext
                    .UserReviews
                    .FirstOrDefaultAsync(x => x.ReviewId.Equals(review.Id));

                var userName = review.UserName;

                var product = await this.DbContext.Products
                    .Select(x => new
                    {
                        x.Id,
                        x.Name
                    })
                    .FirstOrDefaultAsync(x => x.Id.Equals(user_review.ProductId));

                model.Add(new AdminReviewBaseModel
                {
                    AuthorName = userName,
                    ProductName = product.Name,
                    ProductId = product.Id,
                    ReviewId = review.Id,
                    CreatedOn = review.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                });
            }

            return model;
        }


        public async Task SubmitReviewAsync(string username, ProductDetailsModel model)
        {
            var productId = model.Id;

            var user = await this.DbContext
                .BabyBugUsers
                .Select(x => new
                {
                    x.Id,
                    x.UserName
                })
                .FirstOrDefaultAsync(x => x.UserName.Equals(username));

            var review = new Review()
            {
                UserName = model.ReviewModel.AuthorName,
                Content = model.ReviewModel.Content,
                Rating = model.ReviewModel.Rating,
            };

            await this.DbContext.Reviews.AddAsync(review);

            await this.DbContext.SaveChangesAsync();

            var user_review = new UserReviews()
            {
                ReviewId = review.Id,//see if review id is correct
                UserId = user.Id,
                ProductId = productId,
            };

            await this.DbContext.UserReviews.AddAsync(user_review);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task<AdminReviewDetailsModel> GetReviewDetailsModelAsync(int id)
        {
            var review = await this.DbContext
                .Reviews
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var user_review = await this.DbContext
                .UserReviews
                .FirstOrDefaultAsync(x => x.ReviewId.Equals(id));

            var product = await this.DbContext.Products
                    .Select(x => new
                    {
                        x.Id,
                        x.Name
                    })
                    .FirstOrDefaultAsync(x => x.Id.Equals(user_review.ProductId));

            var model = new AdminReviewDetailsModel
            {
                AuthorName = review.UserName,
                Content = review.Content,
                CreatedOn = review.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                ProductId = user_review.ProductId,
                ProductName = product.Name,
                ReviewId = review.Id,
                Rating = review.Rating,
            };

            return model;
        }

        public async Task ApproveReviewAsync(int id)
        {
            var review = await this.DbContext
                .Reviews
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            review.Status = ReviewStatus.Approved;

            await this.DbContext.SaveChangesAsync();
        }

        public async Task RemoveReviewAsync(int id)
        {
            var review = await this.DbContext
                .Reviews
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            this.DbContext.Reviews.Remove(review);

            await this.DbContext.SaveChangesAsync();
        }
    }
}
