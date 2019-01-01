using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Services.Categories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public async Task<ActionResult> Index()
        {
            var categories = await this.categoryService
                .GetAllGarmentCategories();

            return View(categories);
        }
    }
}