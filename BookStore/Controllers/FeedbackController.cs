using BusinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookStore.Controllers
{
    [ApiController]  // Handle the Client error, Bind the Incoming data with parameters using more attribute
    [Route("[controller]")]

    public class FeedbackController : Controller
    {
        private readonly IFeedbackBL feedbackBL;

        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.feedbackBL.AddFeedback(feedbackModel, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Feedback Added SuccessFully", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Feedback Failed" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }

        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetFeedback/{BookId}")]
        public IActionResult GetFeedback(int BookId)
        {
            try
            {

                var cartData = this.feedbackBL.GetFeedback(BookId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Feedback List fetched successful ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Sorry! Failed to fetch" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

    }
}
