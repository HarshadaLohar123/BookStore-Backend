using BusinessLayer.Interface;
using DatabaseLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Registration")]
        public IActionResult Registration(UserRegModel userRegModel)
        {
            try
            {
                UserRegModel userData = this.userBL.Registration(userRegModel);
                if (userData != null)
                {
                    return this.Ok(new { Success = true, message = "User Added Sucessfully", Response = userData });
                }
                return this.Ok(new { Success = true, message = "Sorry! User Already Exists" });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginModel userLoginModel)
        {
            try
            {
                var result = this.userBL.Login(userLoginModel);
                if (result != null)
                    return this.Ok(new { success = true, message = "Login Successful", token = result });
                else
                    return this.BadRequest(new { success = false, message = "Sorry! Login Failed", data = result });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(ForgotPasswordModel forgotPassword)
        {
            try
            {
                var token = this.userBL.ForgotPassword(forgotPassword);
                if (token != null)
                { 
                    return this.Ok(new { success = true, message = "Mail Send Successfully", Token = token });

                }
                return this.BadRequest(new { success = false, message = "Sorry! Sending Failed", Token = token });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [Authorize]
        [HttpPatch("ChangePassword")]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {
            try
            {

                var Email = User.FindFirst("Email").Value;
                var res = userBL.ResetPassword(resetPassword, Email);

                if (res.ToLower().Contains("success"))
                {
                    return Ok(new { success = true, message = res, });
                }
                else
                {
                    return BadRequest(new { success = true, message = res, });
                }
            }
            catch (System.Exception ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
        }


    }

}