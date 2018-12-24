﻿using System;
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
    [Authorize(Roles ="Admin")]
    public class GarmentsController : Controller
    {
        private readonly IGarmentsService garmentService;

        public GarmentsController(IGarmentsService garmentService)
        {
            this.garmentService = garmentService;
        }

        public ActionResult Index()
        {
            return View();
        }        

        public ActionResult Create()
        {
            var model = this.garmentService.GetGarmentCreateModel();
            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateGarmentModel model)
        {
            if (!this.ModelState.IsValid)
            {
                //TODO implement better response
                return this.View();
            }

            await this.garmentService.CreateGarmentAsync(model);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await this.garmentService.GetEditModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(int id, EditGarmentModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentService.EditGarmentAsync(id, model);

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public async Task<ActionResult> Delete(int id)
        {
            var model = await this.garmentService.GetDeleteModelAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(DeleteGarmentModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.garmentService.DeleteGarmentAsync(model.Id);

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}