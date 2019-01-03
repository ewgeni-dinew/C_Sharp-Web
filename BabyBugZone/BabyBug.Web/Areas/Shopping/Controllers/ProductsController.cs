using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.ProductCatalog;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Shopping.Controllers
{
    [Area("Shopping")]
    public class ProductsController : Controller
    {
        private readonly IProductCatalogService catalogService;

        private readonly IProductService productService;

        public ProductsController(
            IProductCatalogService catalogService,
            IProductService productService)
        {
            this.catalogService = catalogService;
            this.productService = productService;
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await this.productService
                .GetDetailsModelAsync(id);

            return View(model);
        }

        public async Task<ActionResult> Index()
        {
            var model = await this.catalogService.GetHomeViewModel();

            return View(model);
        }

        public ActionResult IndexFilter(HomeCatalogModel model)
        {
            return View("Index", model);
        }

        public async Task<ActionResult> FilterByType(string type)
        {
            var model = await this.catalogService.GetHomeModelByTypeAsync(type);

            return this.View("Index", model);
        }

        public async Task<ActionResult> FilterByCategory(string name)
        {
            var model = await this.catalogService.GetHomeModelByCategoryAsync(name);

            return this.View("Index", model);
        }

        public async Task<ActionResult> FilterByCriteria(FilterProductsModel model)
        {
            var homeModel = await this.catalogService.GetHomeModelByCriteriaAsync(model);

            return this.RedirectToAction("IndexFilter", "Products", new { area = "Shopping", model });
        }
    }
}