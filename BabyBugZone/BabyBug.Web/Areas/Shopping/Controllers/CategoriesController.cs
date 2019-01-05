using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Services.Categories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Shopping.Controllers
{
    [Area("Shopping")]
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
                .GetAllProductCategories();

            return View(categories);
        }
    }
}