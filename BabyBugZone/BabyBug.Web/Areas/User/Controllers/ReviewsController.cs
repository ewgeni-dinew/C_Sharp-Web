using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BabyBug.Common.Constants;
using BabyBug.Common.ViewModels.Garments;
using BabyBug.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BabyBug.Web.Areas.User.Controllers
{
    [Area(AreaConstants.USER)]
    [Authorize()]
    public class ReviewsController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<ActionResult> Submit(string username, ProductDetailsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException("Ops...Something went wrong with your review.");
                }
                await this.reviewService
                    .SubmitReviewAsync(username, model);

                //redirect to product/details/id?
                return this.RedirectToAction("Details", "Products", new { area = "Shopping", id = model.Id });
            }
            catch (Exception ex)
            {
                return this.View("Error", ex.Message);
            }

        }
    }
}