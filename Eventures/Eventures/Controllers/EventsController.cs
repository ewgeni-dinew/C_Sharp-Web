using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eventures.Data;
using Eventures.Models;
using Eventures.Services;
using Eventures.Services.Contracts;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class EventsController : Controller
    {
        private EventuresDbContext Db;
        private IErrorExtractor extractor;

        public EventsController(EventuresDbContext db, IErrorExtractor extractor)
        {
            this.Db = db;
            this.extractor = extractor;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateEventBindingModel model)
        {
            if (this.ModelState.IsValid)
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
            else
            {
                var errorModel = new ErrorViewModel()
                {
                    Message = extractor.ExtractErrors(ModelState.Values)
                };

                return this.View("Error", errorModel);
            }
        }

        [Authorize]
        public ActionResult My()
        {
            var events = this.Db
                .Orders
                .Where(x=>x.User.UserName.Equals(this.User.Identity.Name))
                .ToList();

            var list = new List<BaseMyEventViewModel>();

            foreach (var e in events)
            {
                var eventureEvent = this.Db
                    .Events
                    .FirstOrDefault(x => x.Id.Equals(e.EventId))
                    .Name;

                var myEvent = new BaseMyEventViewModel
                {
                    Name = eventureEvent,
                    Start = e.Event.Start,
                    End = e.Event.End,
                    Tickets = e.TicketsCount,
                };

                list.Add(myEvent);
            }

            var model = new AllEventsViewModel<BaseMyEventViewModel>
            {
                Events = list
            };

            return this.View(model);
        }

        [Authorize]
        public ActionResult All()
        {
            var events = this.Db.Events.ToList();

            var list = new List<BaseEventViewModel>();

            foreach (var e in events)
            {
                var baseEvent = new BaseEventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    PricePerTicket = e.PricePerTicket,
                    Start = e.Start,
                    End = e.End,
                    TotalTickets = e.TotalTickets,
                    Place = e.Place,
                    TicketsAmountModel=new TicketsAmountBindingModel { EventId=e.Id}
                };

                list.Add(baseEvent);
            }

            var model = new AllEventsViewModel<BaseEventViewModel>
            {
                Events = list
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Order(int id, TicketsAmountBindingModel model)
        {
            var user = this.Db
                .Users
                .FirstOrDefault(x => x.UserName.Equals(this.User.Identity.Name));

            var eventuresEvent = this.Db
                .Events
                .FirstOrDefault(x => x.Id.Equals(id));

            if (user == null || eventuresEvent == null)
            {
                return this.RedirectToAction("All", "Events");
            }
            
            var order = new EventuresOrder
            {
                User = user,
                UserId=user.Id,
                Event=eventuresEvent,
                EventId=eventuresEvent.Id,
                TicketsCount=model.Tickets
            };

            this.Db.Orders.Add(order);
            this.Db.SaveChanges();

            return this.RedirectToAction("My", "Events");
        }
    }
}