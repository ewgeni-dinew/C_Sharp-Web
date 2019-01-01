﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.TypeManagement;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class TypeManagementController : Controller
    {
        private readonly ITypeManagementService managementService;

        public TypeManagementController(ITypeManagementService managementService)
        {
            this.managementService = managementService;
        }

        public ActionResult Index()
        {
            var model = this.managementService.GetAllTypes();

            return View(model);
        }
        
        public ActionResult Create()
        {
            var model = this.managementService.GetCreateTypeModel();

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreateTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Create");
            }

            await this.managementService
                .CreateTypeAsync(model);

            return this.RedirectToAction("Index");
        }

        // GET: TypeManagement/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TypeManagement/Edit/5
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

        
    }
}