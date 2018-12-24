using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Garments;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;
        
        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Order(int id, GarmentDetailsModel model)
        {
            await this.ordersService.OrderGarment(id, this.User.Identity.Name, model);

            return this.RedirectToAction("Index", "Home");
        }
    }
}