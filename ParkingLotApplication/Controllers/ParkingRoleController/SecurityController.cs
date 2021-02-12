using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotBusinessLayer.IParkingBusinessLayer;
using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {

        private readonly IParkingBusiness securityParking;
        public SecurityController(IParkingBusiness securityParking)
        {
            this.securityParking = securityParking;
        }

        /// <summary>
        /// Security Vehical Parking.
        /// </summary>
        /// <param name="park">The park.</param>
        /// <returns></returns>
        
        [HttpPost]
        [Route("securityVehicalPark")]
        public IActionResult SecurityVehicalPark([FromBody] ParkingModel park)
        {
            try
            {
                var result = this.securityParking.ParkingVehical(park);
                if (result != null)
                {
                    return this.Ok(new { success = true, Message = "Data Added Succesfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, Message = "There is not Empty Slot" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }
    }
}
