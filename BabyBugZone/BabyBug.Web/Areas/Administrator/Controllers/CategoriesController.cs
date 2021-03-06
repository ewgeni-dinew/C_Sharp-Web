﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.Constants;
using BabyBug.Common.ViewModels.Categories;
using BabyBug.Services.Categories.Contracts;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area(AreaConstants.ADMIN)]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService categoriesService;

        public CategoriesController(
            ICategoryService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public ActionResult Create()
        {
            var model = this.categoriesService.GetCreateCategoryModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateCategoryModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.categoriesService.
                    CreateCategoryAsync(model);

                return RedirectToAction("Index", "Categories", new { area = "Shopping" });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
            
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
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction("Edit", new { id });
                }

                await this.categoriesService.EditCategoryAsync(id, model);

                return RedirectToAction("Index", "Categories", new { area = "Shopping" });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await this.categoriesService
                .GetEditCategoryModelAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(int id, EditCategoryModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.RedirectToAction("Delete", new { id });
                }

                await this.categoriesService.DeleteCategoryAsync(id);

                return RedirectToAction("Index", "Categories", new { area = "Shopping" });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }

        }

    }
}