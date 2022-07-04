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

    public class WishListController : Controller
    {
        private readonly IWishListBL wishBL;

        public WishListController(IWishListBL wishBL)
        {
            this.wishBL = wishBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddWishList")]
        public IActionResult AddWishList(WishListModel wishlistModel)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.wishBL.AddWishList(wishlistModel, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added SuccessFully in WishList ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "WishList Add Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteWishList/{WishlistId}")]
        public IActionResult DeleteWishList(int WishlistId)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.wishBL.DeleteWishList(WishlistId, userId);
                if (result != false)
                {
                    return this.Ok(new { status = true, message = $"Delete cart Successful", Data = result });

                }
                return this.BadRequest(new { status = true, message = $" cart delete Failed", Data = result });

            }
            catch(Exception ex)
            {

                throw ex;
            }


        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetWishlistDetailsByUserid")]
        public IActionResult GetWishlistDetailsByUserid()
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.wishBL.GetWishlistDetailsByUserid(userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Wish List fetched successful ", response = cartData });
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
