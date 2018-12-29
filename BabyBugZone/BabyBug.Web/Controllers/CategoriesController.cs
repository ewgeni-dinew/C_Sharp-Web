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
        private readonly ICategoriesService categoryService;

        public CategoriesController(ICategoriesService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var categories = this.categoryService
                .GetAllGarmentCategories();

            return View(categories);
        }
    }
}