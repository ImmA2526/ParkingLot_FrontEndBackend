using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingLotApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        public IUserBusiness business;
 
        public OwnerController(IUserBusiness business)
        {
            this.business = business;
        }

        /// <summary>
        /// Vehical Parking For Owner.
        /// </summary>
        /// <param name="park">The park.</param>
        /// <returns></returns>

        [HttpPost]
        [Route("ownerVehicalPark")]
        public IActionResult OwnerParking([FromBody] DriverTypeModel park)
        {
            try
            {
                var result = this.business.OwnerParkingVehical(park);
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
