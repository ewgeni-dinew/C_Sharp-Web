using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<ActionResult> Index()
        {
            var model = await this.reviewService.GetAllCreatedReviewsModelAsync();

            return this.View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            var model = await this.reviewService.GetReviewDetailsModelAsync(id);

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Approve(int id)
        {
            await this.reviewService.ApproveReviewAsync(id);

            return this.RedirectToAction("Index", "Reviews");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Remove(int id)
        {
            await this.reviewService.RemoveReviewAsync(id);

            return this.RedirectToAction("Index", "Reviews");
        }
    }
}