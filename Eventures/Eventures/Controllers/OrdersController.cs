using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class OrdersController : Controller
    {
        private readonly EventuresDbContext Db;

        public OrdersController(EventuresDbContext Db)
        {
            this.Db = Db;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult All()
        {
            var orders = this.Db.Orders.ToList();

            var list = new List<BaseOrderViewModel>();

            foreach (var o in orders)
            {
                var eventureEventName = this.Db
                    .Events
                    .FirstOrDefault(x => x.Id.Equals(o.EventId))
                    .Name;

                var customerName = this.Db
                    .Users
                    .FirstOrDefault(x => x.Id.Equals(o.UserId))
                    .UserName;

                var order = new BaseOrderViewModel
                {
                    Name = eventureEventName,
                    Customer = customerName,
                    OrderedOn = o.OrderedOn,
                };

                list.Add(order);
            }

            var model = new AllEventsViewModel<BaseOrderViewModel>
            {
                Events = list
            };

            return this.View(model);
        }
    }
}