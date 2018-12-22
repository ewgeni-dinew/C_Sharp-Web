﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BabyBug.Common.ViewModels.Categories
{
    public class EditCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IFormFile Picture { get; set; }
    }
}
