using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Garments;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly IProductService productService;

        public ProductsController(IProductService garmentService)
        {
            this.productService = garmentService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await this.productService.GetOutOfStockProductsModelAsync();

            return this.View(model);
        }

        public ActionResult Create()
        {
            var model = this.productService
                .GetProductCreateModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateProductModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.productService
                    .CreateProductAsync(model);

                return RedirectToAction("Index", "Products", new { area = "Administrator" });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }

        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await this.productService.GetEditModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(int id, EditProductModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.productService.EditProductAsync(id, model);

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }

        }

        public async Task<ActionResult> Delete(int id)
        {
            var model = await this.productService.GetDeleteModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(DeleteProductModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.productService.DeleteProductAsync(model.Id);

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }

        }
    }
}