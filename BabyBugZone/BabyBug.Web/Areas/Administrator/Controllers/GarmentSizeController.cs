using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.GarmentSize;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class GarmentSizeController : Controller
    {
        private readonly IGarmentSizeService garmentSizeService;

        public GarmentSizeController(IGarmentSizeService garmentSizeService)
        {
            this.garmentSizeService = garmentSizeService;
        }

        public ActionResult All()
        {
            var model = this.garmentSizeService.GetAllGarmentSizes();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateGarmentSizeModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentSizeService.CreateSizeAsync(model);

            return this.RedirectToAction("All", "GarmentSize");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await this.garmentSizeService.DeleteSizeAsync(id);

            return this.RedirectToAction("All", "GarmentSize");
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
        public async Task<ActionResult> Edit(BaseGarmentSizeModel model)
        {
            await this.garmentSizeService.EditSizeAsync(model);

            return this.RedirectToAction("All", "GarmentSize");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> AddSizes(int id, GarmentManageSizesModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentSizeService.AddQuantityToGarmentAsync(id, model);

            return this.RedirectToAction("ManageSizes", "GarmentSize", new { id });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> RemoveSizes(int id, GarmentManageSizesModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentSizeService.RemoveQuantityFromGarmentAsync(id, model);

            return this.RedirectToAction("ManageSizes", "GarmentSize", new { id });
        }
    }
}