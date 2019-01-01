using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Garments
{
    public class EditProductModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public char Gender { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CreatedOn { get; set; }

        public HashSet<string> CategoryNames { get; set; }

        public string CategoryName { get; set; }

        public IFormFile Picture { get; set; }
    }
}
