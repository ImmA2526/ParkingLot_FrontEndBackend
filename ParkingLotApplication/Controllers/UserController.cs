using Microsoft.AspNetCore.Mvc;
using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApplication.Controllers
{
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserBusiness business;

        public UserController(IUserBusiness business)
        {
            this.business = business;
        }

        /// <summary>
        /// Register User .
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("api/addUser")]
        public IActionResult AddUser([FromBody] UserModel user)
        {
            try
            {
                var result = this.business.UserRegistration(user);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Data Added successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Data is Not Added Succesfully " });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Login the user.
        /// </summary>
        /// <param name="login">The login.</param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("api/loginUser")]
        public IActionResult LoginUser([FromBody] LoginModel login)
        {
            try
            {
                var result = this.business.UserLogin(login);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Login Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Login Failed" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Forgot password.
        /// </summary>
        /// <param name="forgot">The forgot.</param>
        /// <returns></returns>

        [HttpGet]
        [Route("api/forgotPassword")]
        public IActionResult ForgotPassword(ForgotModel forgot)
        {
            try
            {
                var result = this.business.ForgotUserPassword(forgot);

                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Password Send Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Sending Password Failed" });
                }
            }

            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Reset password.
        /// </summary>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>

        [HttpPut]
        [Route("api/resetPassword")]
        public IActionResult ResetPassword(LoginModel reset)
        {
            try
            {
                var result = this.business.ResetUserPassword(reset);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Password Reset Succesfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Error Ehile Reseting Password" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
