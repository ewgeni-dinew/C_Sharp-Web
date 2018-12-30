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

            return RedirectToAction("Index", "Categories", new { area = "" });
        }

        public async Task<ActionResult> EditGarment(int id)
        {
            var model = await this.categoriesService
                .GetEditGarmentCatModelAsync(id);

            return View("Edit", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> EditGarment(int id, EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditGarment", new { id });
            }

            await this.categoriesService.EditGarmentCategoryAsync(id, model);

            return RedirectToAction("Index", "Categories", new { area = "" });
        }

        public async Task<ActionResult> EditShoe(int id)
        {
            var model = await this.categoriesService
                .GetEditShoeCatModelAsync(id);

            return View("Edit", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> EditShoe(int id, EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditShoe", new { id });
            }

            await this.categoriesService.EditShoeCategoryAsync(id, model);

            return RedirectToAction("Index", "Categories", new { area = "" });
        }

        public async Task<ActionResult> EditAccessory(int id)
        {
            var model = await this.categoriesService
                .GetEditAccessoryCatModelAsync(id);

            return View("Edit", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> EditAccessory(int id, EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("EditAccessory", new { id });
            }

            await this.categoriesService.EditAccessoryCategoryAsync(id, model);

            return RedirectToAction("Index", "Categories", new { area = "" });
        }

        [HttpGet]
        public async Task<ActionResult> DeleteGarment(int id)
        {
            var model = await this.categoriesService
                .GetEditGarmentCatModelAsync(id);

            return View("Delete", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> DeleteGarment(int id, DeleteCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("DeleteGarment", new { id });
            }

            await this.categoriesService.DeleteGarmentCategoryAsync(id);

            return RedirectToAction("Index", "Categories", new { area = "" });
        }

        [HttpGet]
        public async Task<ActionResult> DeleteShoe(int id)
        {
            var model = await this.categoriesService
                .GetEditShoeCatModelAsync(id);

            return View("Delete", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> DeleteShoe(int id, EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("DeleteShoe", new { id });
            }

            await this.categoriesService.DeleteGarmentCategoryAsync(id);

            return RedirectToAction("Index", "Categories", new { area = "" });
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAccessory(int id)
        {
            var model = await this.categoriesService
                .GetEditAccessoryCatModelAsync(id);

            return View("Delete", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> DeleteAccessory(int id, EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("DeleteAccessory", new { id});
            }

            await this.categoriesService.DeleteAccessoryCategoryAsync(id);

            return RedirectToAction("Index", "Categories", new { area = "" });
        }
    }
}