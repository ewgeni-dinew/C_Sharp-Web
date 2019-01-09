using BabyBugZone.Data;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Services.Contracts
{
    public interface IBaseCloudinaryService : IBaseDbService
    {
        Account Account { get; set; }

        Cloudinary Cloudinary { get; set; }

        ImageUploadResult UploadImageToCloudinary(IFormFile file, string path);

        void RemoveImageFromCloudinary(string imageId);
    }
}
