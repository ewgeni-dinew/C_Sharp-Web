using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.ProductSize;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class ProductSizeController : Controller
    {
        private readonly ISizeService garmentSizeService;

        public ProductSizeController(ISizeService garmentSizeService)
        {
            this.garmentSizeService = garmentSizeService;
        }

        public ActionResult All()
        {
            var model = this.garmentSizeService
                .GetAllGarmentSizes();

            return View(model);
        }

        public ActionResult Create()
        {
            var model = this.garmentSizeService
                .GetCreateSizeModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateProductSizeModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentSizeService.CreateSizeAsync(model);

            return this.RedirectToAction("All");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await this.garmentSizeService.DeleteSizeAsync(id);

            return this.RedirectToAction("All");
        }

        public async Task<ActionResult> ManageSizes(int id)
        {
            var model = await this.garmentSizeService.GetCurrentGarmentSizeDetails(id);

            return this.View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await this.garmentSizeService.GetBaseSizeModelAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(BaseProductSizeModel model)
        {
            await this.garmentSizeService.EditSizeAsync(model);

            return this.RedirectToAction("All");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> AddSizes(int id, ProductManageSizesModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentSizeService.AddQuantityToGarmentAsync(id, model);

            return this.RedirectToAction("ManageSizes", "ProductSize", new { id });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> RemoveSizes(int id, ProductManageSizesModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentSizeService.RemoveQuantityFromGarmentAsync(id, model);

            return this.RedirectToAction("ManageSizes", "ProductSize", new { id });
        }
    }
}