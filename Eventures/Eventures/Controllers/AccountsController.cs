using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Eventures.Models;
using Eventures.Services.Contracts;
using Eventures.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Eventures.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;
        private SignInManager<EventuresUser> signInManager;
        private IErrorExtractor extractor;

        public AccountsController(
            IAuthenticationSchemeProvider authenticationSchemeProvider,
            SignInManager<EventuresUser> signInManager,
            IErrorExtractor extractor)
        {
            this.authenticationSchemeProvider = authenticationSchemeProvider;
            this.extractor = extractor;
            this.signInManager = signInManager;
        }

        public ActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Register(RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new EventuresUser()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    UCN = model.UCN,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
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
            else
            {
                var errorModel = new ErrorViewModel()
                {
                    Message = extractor.ExtractErrors(ModelState.Values)
                };

                return this.View("Error", errorModel);
            }
        }

        public async Task<ActionResult> Login()
        {
            var allSchemeProvider = (await authenticationSchemeProvider
                .GetAllSchemesAsync())
                .Select(x => x.DisplayName)
                .Where(x => !String.IsNullOrEmpty(x));

            var model = new LoginBindingModel { ExternalLinks = allSchemeProvider };

            return this.View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginBindingModel model)
        {
            var user = this.signInManager
                .UserManager
                .Users
                .FirstOrDefault(x => x.UserName.Equals(model.Username));

            var claims = new List<Claim>

            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UCN", user.UCN),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            //var result = this.signInManager.PasswordSignInAsync(user.UserName, model.Password, true, false);
            //await result;

            //if (result.IsCompletedSuccessfully)
            //{

            //}

            return this.RedirectToAction("Index", "Home");
        }

        public ActionResult SignIn(string provider)
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/" }, provider);
        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync();
            this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Home");
        }
    }
}