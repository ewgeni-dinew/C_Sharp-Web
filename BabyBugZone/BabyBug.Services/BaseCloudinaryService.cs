using BabyBugZone.Data;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Services
{
    public abstract class BaseCloudinaryService
    {
        protected const string BASE_PATH = @"https://res.cloudinary.com/dm6qsz74d/image/upload/v1545506271/";

        protected BaseCloudinaryService(BabyBugDbContext DbContext)
        {
            this.Account = new Account(
                "dm6qsz74d",
                "468634173751356",
                "M-LBxFKAP9qqhMzNd8Sgsx5RxE8");

            this.Cloudinary = new Cloudinary(this.Account);

            this.DbContext = DbContext;
        }

        public BabyBugDbContext DbContext { get; set; }

        public Account Account { get; set; }

        public Cloudinary Cloudinary { get; set; }

        protected ImageUploadResult UploadImageToCloudinary(IFormFile file, string path)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                Folder = path
            };

            var uploadResult = this.Cloudinary.Upload(uploadParams);

            return uploadResult;
        }

        protected void RemoveImageFromCloudinary(string imageId)
        {
            var imageIdParams = new DeletionParams(imageId);

            var deleteResult = this.Cloudinary.Destroy(imageIdParams);
        }
    }
}
