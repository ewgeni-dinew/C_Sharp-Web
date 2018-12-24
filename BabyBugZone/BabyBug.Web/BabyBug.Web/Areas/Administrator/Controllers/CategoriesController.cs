using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Categories;
using BabyBug.Services.Categories.Contracts;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(
            ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.CreateCategoryAsync(model);

            return RedirectToAction("All", "Categories", new { area = "" });
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await this.categoriesService
                .GetEditCategoryModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(int id, EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.EditCategoryAsync(id, model);

            return RedirectToAction("All", "Categories", new { area = "" });
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await this.categoriesService
                .GetDeleteCategoryModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(int id, DeleteCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.DeleteCategoryAsync(id);

            return RedirectToAction("All", "Categories", new { area = "" });
        }
    }
}