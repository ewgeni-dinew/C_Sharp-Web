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

        public async Task<ActionResult> Awaiting()
        {
            var model = await this.ordersService.GetAwaitingOrdersAsync();

            return View(model);
        }

        public async Task<ActionResult> Approved()
        {
            var model = await this.ordersService.GetApprovedOrdersAsync();

            return View(model);
        }

        public async Task<ActionResult> DetailsAwaiting(int id)
        {
            var model = await this.ordersService.GetAwaitingOrderedProductsAdminAsync(id);

            return this.View("Details", model);
        }

        public async Task<ActionResult> DetailsApproved(int id)
        {
            var model = await this.ordersService.GetApprovedOrderedProductsAdminAsync(id);

            return this.View("Details", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Approve(int id)
        {
            await this.ordersService.ApproveOrderAsync(id);

            return this.RedirectToAction("All", "Orders");
        }
    }
}