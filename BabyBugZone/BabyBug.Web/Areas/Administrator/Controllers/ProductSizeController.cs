using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.Constants;
using BabyBug.Common.ViewModels.ProductSize;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area(AreaConstants.ADMIN)]
    [Authorize(Roles = RoleConstants.ADMIN)]
    public class ProductSizeController : Controller
    {
        private readonly ISizeService garmentSizeService;

        public ProductSizeController(ISizeService garmentSizeService)
        {
            this.garmentSizeService = garmentSizeService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await this.garmentSizeService
                .GetAllProductSizesAsync();

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
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.garmentSizeService.CreateSizeAsync(model);

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await this.garmentSizeService.DeleteSizeAsync(id);

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }

        public async Task<ActionResult> ManageSizes(int id, int typeId)
        {
            var model = await this.garmentSizeService
                .GetCurrentProductSizeDetailsAsync(id, typeId);

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
            try
            {
                await this.garmentSizeService.EditSizeAsync(model);

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> AddSizes(int id, ProductManageSizesModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.garmentSizeService.AddQuantityToProductAsync(id, model);

                return this.RedirectToAction("ManageSizes", "ProductSize", new { id, typeId = model.TypeId });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> RemoveSizes(int id, ProductManageSizesModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.garmentSizeService.RemoveQuantityFromProductAsync(id, model);

                return this.RedirectToAction("ManageSizes", "ProductSize", new { id, typeId = model.TypeId });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }
    }
}