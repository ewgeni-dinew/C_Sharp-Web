using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        public ActionResult All()
        {
            var model = this.ordersService.GetAllOrders();

            return View(model);
        }

        public ActionResult Details(string username)
        {
            var model = this.ordersService.GetOrderedProducts(username);

            return this.View(model);
        }
    }
}