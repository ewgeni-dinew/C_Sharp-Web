using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Orders;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize()]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Order(int id, GarmentDetailsModel model)
        {
            await this.ordersService.OrderGarment(id, this.User.Identity.Name, model);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize()]
        public ActionResult MyOrders()
        {
            var model = this.ordersService.GetOrderedProducts(this.User.Identity.Name);

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> RemoveProduct(int orderId, int productId, string size)
        {
            await this.ordersService.RemoveProductFromOrder(orderId, productId, size);

            return this.RedirectToAction("MyOrders", "Orders");
        }

        [Authorize()]
        public async Task<ActionResult> ManageUser(string username)
        {
            var model = await this.ordersService.GetUserDataModel(username);

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> ManageUser(UserDataModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("ManageUser", "Orders", new { username = this.User.Identity.Name });
            }

            await this.ordersService.UpdateUserInfo(model);

            return this.RedirectToAction("ManageDelivery", "Orders", new { username = this.User.Identity.Name });
        }

        public async Task<ActionResult> ManageDelivery(string username)
        {
            //var model = await this.ordersService.GetDeliveryModel();

            throw new NotImplementedException();
        }
    }
}