using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BabyBug.Web.Models.Categories
{
    public class BaseCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile Picture { get; set; }
    }
}
