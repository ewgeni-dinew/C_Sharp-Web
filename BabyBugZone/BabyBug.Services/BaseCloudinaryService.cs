using BabyBug.Common.Validation;
using BabyBugZone.Data;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BabyBug.Services
{
    public abstract class BaseCloudinaryService : BaseDbService
    {
        protected const string BASE_PATH = @"https://res.cloudinary.com/dm6qsz74d/image/upload/v1545506271/";

        protected BaseCloudinaryService(BabyBugDbContext DbContext)
            : base(DbContext)
        {
            this.Account = new Account(
                "dm6qsz74d",
                "468634173751356",
                "M-LBxFKAP9qqhMzNd8Sgsx5RxE8");

            this.Cloudinary = new Cloudinary(this.Account);
        }

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

        protected bool IsValidImageFile(IFormFile file)
        {
            /////Uncomment when implemented work with IFormFileCollection!
            //if (files.Count == 0)
            //{
            //    throw new ArgumentException("Please Upload Your file!");
            //}
            //else
            //{
            //    foreach (var file in files)
            //    {

            if (file.Length > 0)
            {

                byte[] tempFileBytes = null;
                var fileName = file.FileName.Trim();

                using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
                {
                    tempFileBytes = reader.ReadBytes((int)file.Length);
                }

                var myUniqueFileName = Convert.ToString(Guid.NewGuid());

                var filetype = Path.GetExtension(fileName).Replace('.', ' ').Trim();

                var fileExtension = Path.GetExtension(fileName);

                // Setting Image type
                var types = FileUploadCheck.FileType.Image;

                // Validate Header
                var result = FileUploadCheck.IsValidFile(tempFileBytes, types, filetype);

                return result;
            }

            return false;
        }
    }
}
