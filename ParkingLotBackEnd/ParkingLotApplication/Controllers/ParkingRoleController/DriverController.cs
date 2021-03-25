using Microsoft.AspNetCore.Authorization;
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
    //[Authorize(Roles = "Driver,Policeman")]
    public class DriverController : ControllerBase
    {

        private readonly IParkingBusiness driverParking;
        public DriverController(IParkingBusiness driverParking)
        {
            this.driverParking = driverParking;
        }

        /// <summary>
        /// Vehical Parking For Owner.
        /// </summary>
        /// <param name="park">The park.</param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult DriverVehicalPark([FromBody] ParkingModel park)
        {
            try
            {
                var result = this.driverParking.ParkingVehical(park);
                if (result != null && park.DriverTypeID==1)
                {
                    return this.Ok(new { Status = true, Message = "Parking Succesfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error While Parking" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Unpark Vehical if the entry is true.
        /// </summary>
        /// <param name="unpark">The unpark.</param>
        /// <returns></returns>

        [HttpPut]
        public IActionResult DriverVehicalUnpark(int id)
        {
            try
            {
                var unparks = this.driverParking.UnparkingVehical(id);

                if (unparks.IsEmpty == false)
                {
                    return this.Ok(new { Status = true, Message = "Park", Data = unparks });
                }
                if (unparks.IsEmpty == true)
                {
                    return this.Ok(new { Status = true, Message = "UnPark", Data = unparks });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error While UnParking" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
    }
}
