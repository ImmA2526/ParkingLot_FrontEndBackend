using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly IUserBusiness business;
        private readonly IConfiguration configuration;
        private IDistributedCache cache;
        private string cacheKey;
        private DistributedCacheEntryOptions options;
        public UserController(IUserBusiness business, IConfiguration configuration, IDistributedCache cache)
        {
            this.business = business;
            this.configuration = configuration;
            this.cache = cache;
            this.options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromHours(3.0));
            this.cacheKey = "parkingLotUser";

        }

        /// <summary>
        /// Genrates the JWT token.
        /// </summary>
        /// <param name="Role">The role.</param>
        /// <param name="Email">The email.</param>
        /// <returns></returns>
        private string GenrateJWTToken(string Role, string Email)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Key"]));
            var signinCredentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, Role),
                new Claim("Email",Email)
            };
            var tokenOptionOne = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
                );
            string token = new JwtSecurityTokenHandler().WriteToken(tokenOptionOne);
            return token;
        }

        /// <summary>
        /// Register User .
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>

        [HttpPost]
        [Route("registerUser")]
        public IActionResult RegisterUser([FromBody] UserModel user)
        {
            try
            {
                var result = this.business.UserRegistration(user);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Data Added Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Data is Not Added Succesfully " });
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
        [Route("loginUser")]
        public async Task<IActionResult> LoginUser([FromBody] LoginModel login)
        {
            try
            {
                //Redis Code For Login
                var cacheKey = "loginData";
                var loginData = new List<LoginModel>();
                var redis = await cache.GetAsync(cacheKey);
                if (string.IsNullOrEmpty(cache.GetString("loginData")))
                {
                    
                }

                var result = this.business.UserLogin(login);
                if (result != null)
                {
                    var token = GenrateJWTToken(result.Role, result.Email);
                    return this.Ok(new { Status = true, Message = "Login Successfully", Data = token });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Login Failed" });
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
        [AllowAnonymous]
        [HttpGet]
        [Route("forgotPassword")]
        public IActionResult ForgotPassword([FromQuery] ForgotModel forgot)
        {
            try
            {
                var result = this.business.ForgotUserPassword(forgot);

                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Password Send Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Sending Password Failed" });
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
        /// <param name="reset">The reset.</param>
        /// <returns></returns>

        [HttpPut]
        [Route("resetPassword")]
        public IActionResult ResetPassword(LoginModel reset)
        {
            try
            {
                var result = this.business.ResetUserPassword(reset);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Password Reset Succesfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error While Reseting Password" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        //Types Of Data Being Added //
        ///// <summary>
        ///// Adding Type of Vehical.
        ///// </summary>
        ///// <param name="vehical">The vehical.</param>
        ///// <returns></returns>

        [HttpPost]
        [Route("vehicalType")]
        public IActionResult VehicalType([FromBody] VehicalTypeModel vehical)
        {
            try
            {
                var result = this.business.VehicalTypes(vehical);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = "Vehical Type Added Sucesfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "Error While Adding" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, message = e.Message });
            }
        }

        ///// <summary>
        ///// Adding Type of Driver.
        ///// </summary>
        ///// <param name="vehical">The vehical.</param>
        ///// <returns></returns>

        [HttpPost]
        [Route("driverType")]
        public IActionResult DriverType([FromBody] DriverTypeModel driver)
        {
            try
            {
                var result = this.business.DriverTypes(driver);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = "Driver Type Added Sucesfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "Error While Adding Driver" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, message = e.Message });
            }
        }

        ///// <summary>
        ///// Adding Type of Parking.
        ///// </summary>
        ///// <param name="vehical">The vehical.</param>
        ///// <returns></returns>//

        [HttpPost]
        [Route("parkingType")]
        public IActionResult ParkingType([FromBody] ParkingTypeModel parking)
        {
            try
            {
                var result = this.business.ParkingTypes(parking);
                if (result != null)
                {
                    return this.Ok(new { Status = true, message = "Parking Data Added sucesfully", data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, message = "Error while Adding Parking Data" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, message = e.Message });
            }
        }
    }
}