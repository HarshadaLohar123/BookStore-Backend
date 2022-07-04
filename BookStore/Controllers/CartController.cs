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

    public class CartController : Controller
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }
        [Authorize(Roles = Role.User)]
        [HttpPost("AddCart")]
        public IActionResult AddCart(CartModel cart)
        {
            try
            {
                int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cartData = this.cartBL.AddCart(cart, userId);
                if (cartData != null)
                {
                    return this.Ok(new { success = true, message = "Book Added SuccessFully in Cart ", response = cartData });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Cart Add Unsuccessfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, response = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteBook/{CartId}")]
        public IActionResult RemoveFromCart(int CartId)
        {
            try
            {
                //int userId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = this.cartBL.RemoveFromCart(CartId);
                if (result != null)
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
        [HttpGet("GetCartDetailsByUserid")]
        public IActionResult GetCartDetailsByUserid()
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var cart = this.cartBL.GetCartDetailsByUserid(UserId);
                if (cart != null)
                {
                    return this.Ok(new { Success = true, message = "cart Detail Fetched Sucessfully", Response = cart });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Soory! Please Enter Valid UserId" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateCart/{CartId}")]
        public IActionResult UpdateCart(int CartId, CartModel cartModel)
        {
            try
            {
                int UserId = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                CartModel userData = this.cartBL.UpdateCart(CartId, cartModel, UserId);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "Cart Updeted Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Sorry! Updated Failed" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
    }
}
