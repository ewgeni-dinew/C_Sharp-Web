using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService blogService;

        public BlogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await this.blogService.GetBlogDetailsAsync(id);

            return View(model);
        }

    }
}