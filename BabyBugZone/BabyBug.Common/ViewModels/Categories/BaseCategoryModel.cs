using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBug.Common.ViewModels.Categories
{
    public class BaseCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile Picture { get; set; }

        public string ImageUrl { get; set; }
    }
}
