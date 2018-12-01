using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper mapper;

        public EventsController(
            EventuresDbContext db,
            IErrorExtractor extractor,
            IMapper mapper)
        {
            this.mapper = mapper;
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
                var eventuresEvent = mapper.Map<EventuresEvent>(model);
                
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
                .Where(x => x.User.UserName.Equals(this.User.Identity.Name))
                .ToList();

            var list = new List<BaseMyEventViewModel>();

            foreach (var e in events)
            {
                var eventureEventName = this.Db
                    .Events
                    .FirstOrDefault(x => x.Id.Equals(e.EventId))
                    .Name;

                var myEvent = mapper.Map<BaseMyEventViewModel>(e);
                myEvent.Name = eventureEventName;

                //var myEvent = new BaseMyEventViewModel
                //{
                //    Name = eventureEventName,
                //    Start = e.Event.Start,
                //    End = e.Event.End,
                //    Tickets = e.TicketsCount,
                //};

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
            var events = this.Db
                .Events
                .Where(x => x.TotalTickets >= 0)
                .ToList();

            var list = new List<BaseEventViewModel>();

            foreach (var e in events)
            {
                var baseEvent = mapper.Map<BaseEventViewModel>(e);

                baseEvent.TicketsAmountModel = new TicketsAmountBindingModel { EventId = e.Id };
      
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

            if (eventuresEvent.TotalTickets < model.Tickets)
            {
                var errorViewModel = new ErrorViewModel
                {
                    Message = "Ordered tickets are MORE than event tickets!"
                };

                return this.View("Error", errorViewModel);
            }

            var order = new EventuresOrder
            {
                User = user,
                UserId = user.Id,
                Event = eventuresEvent,
                EventId = eventuresEvent.Id,
                TicketsCount = model.Tickets
            };

            this.Db.Orders.Add(order);
            eventuresEvent.TotalTickets -= order.TicketsCount;
            this.Db.SaveChanges();

            return this.RedirectToAction("My", "Events");
        }
    }
}