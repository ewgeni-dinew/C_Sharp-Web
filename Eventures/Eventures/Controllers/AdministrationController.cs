using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eventures.Data;
using Eventures.Models;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly SignInManager<EventuresUser> signInManager;
        private readonly IMapper mapper;
        private EventuresDbContext Db;

        public AdministrationController(
            SignInManager<EventuresUser> signInManager,
            IMapper mapper,
            EventuresDbContext db)
        {
            this.signInManager = signInManager;
            this.mapper = mapper;
            this.Db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var users = this.Db.Users.Where(x=>!x.UserName.Equals(this.User.Identity.Name));

            var userManager = this.signInManager.UserManager;

            var list = new List<BaseAdministrationModel>();

            foreach (var u in users)
            {
                var role = string.Empty;

                if (userManager.IsInRoleAsync(u, "Admin").Result)
                {
                    role = "Admin";
                }
                else
                {
                    role = "Normal";
                }

                var baseModel = new BaseAdministrationModel
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Role = role
                };

                list.Add(baseModel);
            }

            var viewModel = new AllEventsViewModel<BaseAdministrationModel>()
            {
                Events = list
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Promote(string id)
        {
            var user = this.Db
                .Users
                .FirstOrDefault(x => x.Id.Equals(id));

            this.signInManager.UserManager.AddToRoleAsync(user, "Admin");
            this.Db.SaveChanges();

            return this.RedirectToAction("Index", "Administration");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Denote(string id)
        {
            var user = this.Db
                .Users
                .FirstOrDefault(x => x.Id.Equals(id));

            this.signInManager.UserManager.RemoveFromRoleAsync(user, "Admin");
            this.Db.SaveChanges();

            return this.RedirectToAction("Index", "Administration");
        }
    }
}