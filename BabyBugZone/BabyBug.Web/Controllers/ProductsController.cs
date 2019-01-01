﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Controllers
{
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

        public async Task<ActionResult> Details(int id)
        {
            var model = await this.garmentService
                .GetDetailsModelAsync(id);

            return View(model);
        }
    }
}