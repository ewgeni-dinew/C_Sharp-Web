using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Categories;
using BabyBug.Services.Categories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        
        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateCategoryModel model)
        {        
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.CreateCategoryAsync(model);

            return RedirectToAction("All", "Categories", new { area = "" });
        }

        // GET: Categories/Edit/5
        public ActionResult Edit()
        {
            var model = this.categoriesService
                .GetEditCategoryModel();

            return View(model);
        }

        // POST: Categories/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.EditCategoryAsync(model);

            return RedirectToAction("All", "Categories", new { area = "" });
        }

        // GET: Categories/Delete/5
        [HttpGet]
        public ActionResult Delete()
        {
            var model = this.categoriesService
                .GetDeleteCategoryModel();

            return View(model);
        }

        // POST: Categories/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(EditCategoryModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.categoriesService.DeleteCategoryAsync(model);

            return RedirectToAction("All", "Categories", new { area = "" });
        }
    }
}