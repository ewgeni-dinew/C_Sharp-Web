using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Blog;
using BabyBug.Data.Models;
using BabyBug.Services.Contracts;
using BabyBugZone.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BabyBug.Services
{
    public class BlogService : BaseCloudinaryService, IBlogService
    {
        private const string IMAGE_PATH = @"Blog";

        public BlogService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
        }

        public async Task CreateBlogPageAsync(CreatePageModel model)
        {
            if (!this.IsValidImageFile(model.Picture))
            {
                throw new ArgumentException("Invalid file type!");
            }

            var file = model.Picture;

            //upload image
            var uploadResult = this.UploadImageToCloudinary(file, IMAGE_PATH);

            var page = new BlogPage
            {
                Author = model.Author,
                Header = model.Header,
                Content = model.Content,
                ImageId = uploadResult.PublicId,
                ImageUrl = BASE_PATH + uploadResult.PublicId,
            };

            await this.DbContext.BlogPages.AddAsync(page);

            await this.DbContext.SaveChangesAsync();
        }

        public async Task<PageEditModel> GetEditPageModelByIdAsync(int id)
        {
            var page = await this.DbContext
                .BlogPages
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            var model = new PageEditModel
            {
                Id = page.Id,
                Author = page.Author,
                Header = page.Header,
                PageContent = page.Content,
            };

            return model;
        }

        public async Task EditBlogPageAsync(int id, PageEditModel model)
        {
            if (!this.IsValidImageFile(model.Picture))
            {
                throw new ArgumentException("Invalid file type!");
            }

            var page = await this.DbContext
                .BlogPages
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (model.Author != null)
            {
                page.Author = model.Author;
            }
            if (model.PageContent != null)
            {
                page.Content = model.PageContent;
            }
            if (model.Header != null)
            {
                page.Header = model.Header;
            }
            if (model.Picture != null)
            {
                //update picture

                //remove old image from Cloudinary
                this.RemoveImageFromCloudinary(page.ImageId);

                //upload new image
                var uploadResult = this.UploadImageToCloudinary(model.Picture, IMAGE_PATH);

                page.ImageUrl = BASE_PATH + uploadResult.PublicId;
                page.ImageId = uploadResult.PublicId;
            }

            await this.DbContext.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(int id)
        {
            var page = await this.DbContext
                .BlogPages
                .FirstOrDefaultAsync(x => x.Id.Equals(id));

            page.IsDeleted = true;

            await this.DbContext.SaveChangesAsync();
        }

        public async Task<PageDetailsModel> GetBlogDetailsAsync(int id)
        {
            var page = await this.DbContext
                .BlogPages
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted == false);

            var model = new PageDetailsModel
            {
                Id = page.Id,
                Author = page.Author,
                Header = page.Header,
                Contect = page.Content,
                CreatedOn = page.CreatedOn.ToString("dd/MM/yyyy"),
                ImageUrl = page.ImageUrl
            };

            return model;
        }

        public ICollection<HomePageBlogModel> GetBasePageModelCollection()
        {
            var pages = this.DbContext
                .BlogPages
                .Where(x => x.IsDeleted == false)
                .Select(x => new HomePageBlogModel
                {
                    PageId = x.Id,
                    Author = x.Author,
                    Heading = x.Header,
                    ImageUrl = x.ImageUrl,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy"),
                })
                .ToList();

            return pages;
        }
    }
}
