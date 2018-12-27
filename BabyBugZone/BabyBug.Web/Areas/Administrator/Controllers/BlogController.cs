using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.ViewModels.Blog;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Create(CreatePageModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            await this.blogService.CreateBlogPageAsync(model);

            return this.RedirectToAction("Index", "Blog");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var model = await this.blogService.GetEditPageModelByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Edit(int id, PageEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.RedirectToAction("Edit", "Blog", new { id });
            }

            await this.blogService.EditBlogPageAsync(id, model);

            return this.RedirectToAction("Index", "Home");
        }
        
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            await this.blogService.DeleteBlogAsync(id);

            return this.RedirectToAction("Index", "Home");
        }
    }
}