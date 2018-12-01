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
using AutoMapper;

namespace Eventures.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAuthenticationSchemeProvider authenticationSchemeProvider;
        private SignInManager<EventuresUser> signInManager;
        private IErrorExtractor extractor;
        private readonly IMapper mapper;

        public AccountsController(
            IAuthenticationSchemeProvider authenticationSchemeProvider,
            SignInManager<EventuresUser> signInManager,
            IErrorExtractor extractor,
            IMapper mapper)
        {
            this.mapper = mapper;
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
                var user = mapper.Map<EventuresUser>(model);

                //var user = new EventuresUser()
                //{
                //    UserName = model.Username,
                //    Email = model.Email,
                //    UCN = model.UCN,
                //    FirstName = model.FirstName,
                //    LastName = model.LastName,
                //};

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
            var hasher = new PasswordHasher<EventuresUser>();

            var user = this.signInManager
                .UserManager
                .Users
                .FirstOrDefault(x => x.UserName.Equals(model.Username));

            var role = this.signInManager
                .UserManager
                .GetRolesAsync(user)
                .Result
                .FirstOrDefault();


            if (hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password)
                == PasswordVerificationResult.Failed)
            {
                var errorModel = new ErrorViewModel()
                {
                    Message = "Invalid username or password."
                };

                return this.View("Error", errorModel);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("UCN", user.UCN),
            };

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

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