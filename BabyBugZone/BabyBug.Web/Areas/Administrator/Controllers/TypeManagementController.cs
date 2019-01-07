using System;
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
            var model = this.managementService
                .GetAllTypes();

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
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.RedirectToAction("Create");
                }

                await this.managementService
                    .CreateTypeAsync(model);

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }
        
        public ActionResult Edit(string name)
        {
            var model = this.managementService
                .GetEditTypeModel(name);

            return View(model);
        }
        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(EditTypeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.RedirectToAction("Edit");
                }

                await this.managementService
                    .EditTypeAsync(model);

                return this.RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }
        }        
    }
}