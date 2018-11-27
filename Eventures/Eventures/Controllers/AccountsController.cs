using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventures.Models;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Eventures.Controllers
{
    public class AccountsController : Controller
    {
        private SignInManager<EventuresUser> signInManager;

        public AccountsController(SignInManager<EventuresUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            var user = new EventuresUser()
            {
                UserName = model.Username,
                Email = model.Email,
                UCN=model.UCN,
                FirstName=model.FirstName,
                LastName=model.LastName,
            };

            var result = this.signInManager.UserManager.CreateAsync(user, model.Password).Result;

            if (this.signInManager.UserManager.Users.Count() == 1)
            {
                var adminRoleResult = this.signInManager.UserManager.AddToRoleAsync(user, "Admin").Result;
                if (adminRoleResult.Errors.Any())
                {
                    return this.View();
                }
            }
            if (result.Succeeded)
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View();
        }

        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var user = this.signInManager
                .UserManager
                .Users
                .FirstOrDefault(x => x.UserName.Equals(model.Username));

            this.signInManager.PasswordSignInAsync(user, model.Password, false, false).Wait();

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}