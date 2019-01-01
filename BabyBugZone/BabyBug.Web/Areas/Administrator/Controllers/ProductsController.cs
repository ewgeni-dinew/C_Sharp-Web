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
        private readonly IProductService garmentService;

        public ProductsController(IProductService garmentService)
        {
            this.garmentService = garmentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            var model = this.garmentService
                .GetProductCreateModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateProductModel model)
        {
            if (!this.ModelState.IsValid)
            {
                //TODO implement better response
                return this.View();
            }

            await this.garmentService
                .CreateProductAsync(model);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await this.garmentService.GetEditModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(int id, EditProductModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentService.EditProductAsync(id, model);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var model = await this.garmentService.GetDeleteModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(DeleteProductModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentService.DeleteProductAsync(model.Id);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}