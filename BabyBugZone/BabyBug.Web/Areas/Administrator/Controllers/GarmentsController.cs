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
    public class GarmentsController : Controller
    {
        private readonly IGarmentsService garmentService;

        public GarmentsController(IGarmentsService garmentService)
        {
            this.garmentService = garmentService;
        }
        // GET: Garments
        public ActionResult Index()
        {
            return View();
        }

        // GET: Garments/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var model= await this.garmentService.GetDetailsAsync(id);

            return View(model);
        }

        // GET: Garments/Create
        [Authorize(Roles ="Admin")]
        public ActionResult Create()
        {
            var model = this.garmentService.GetGarmentCreateModel();
            return View(model);
        }

        // POST: Garments/Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateGarmentModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentService.CreateGarmentAsync(model);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Garments/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Garments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Garments/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Garments/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}