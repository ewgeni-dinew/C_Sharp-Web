using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Garments;
using BabyBug.Common.ViewModels.Orders;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize()]
    public class OrdersController : Controller
    {
        private readonly IOrderService ordersService;

        public OrdersController(IOrderService ordersService)
        {
            this.ordersService = ordersService;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Order(int id, ProductDetailsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException("Order not made. Invalid Model State!");
                }
                await this.ordersService
                    .OrderProductAsync(id, this.User.Identity.Name, model);

                return this.RedirectToAction("MyOrders", "Orders");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ActionResult> MyOrders()
        {
            var model = await this.ordersService
                .GetOrderedProductsUserAsync(this.User.Identity.Name);

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> RemoveProduct(int orderId, int productId, string size)
        {
            try
            {
                await this.ordersService
                .RemoveProductFromOrderAsync(orderId, productId, size);

                return this.RedirectToAction("MyOrders", "Orders");
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ActionResult> ManageDelivery(string username)
        {
            var model = await this.ordersService
                .GetUserDataModelAsync(username);

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> ManageDelivery(int id, UserDataModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return this.RedirectToAction("ManageDelivery", "Orders", new { username = this.User.Identity.Name });
                }

                await this.ordersService
                    .SetDeliveryInfoAsync(id, model);

                return this.RedirectToAction("FinishedOrder", "Orders", new { id });
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ActionResult> FinishedOrder(int id)
        {
            var model = await this.ordersService
                .SetOrderDateAsync(id);

            return this.View(model);
        }
    }
}