using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.Models;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private EventuresDbContext Db;

        public EventsController(EventuresDbContext db)
        {
            this.Db = db;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEventViewModel model)
        {
            var eventuresEvent = new EventuresEvent
            {
                Name = model.Name.Trim(),
                PricePerTicket = model.PricePerTicket,
                Start = model.Start,
                End = model.End,
                TotalTickets = model.TotalTickets,
                Place = model.Place
            };

            if (this.Db.Events.Any(x => x.Name.Equals(eventuresEvent.Name)))
            {
                return this.View();
            }

            this.Db.Events.Add(eventuresEvent);
            this.Db.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult All()
        {
            if (this.User.Identity.Name == null)
            {
                return this.RedirectToAction("Register", "Accounts");
            }

            var events = this.Db.Events.ToList();

            var list = new List<BaseEventViewModel>();

            foreach (var e in events)
            {
                var baseEvent = new BaseEventViewModel
                {
                    Name = e.Name,
                    PricePerTicket = e.PricePerTicket,
                    Start = e.Start,
                    End = e.End,
                    TotalTickets = e.TotalTickets,
                    Place = e.Place
                };

                list.Add(baseEvent);
            }

            var model = new AllEventsViewModel
            {
                Events = list
            };

            return this.View(model);
        }
    }
}