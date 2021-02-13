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
    [Authorize(Roles = "Policeman")]
    public class PolicemanController : ControllerBase
    {

        private readonly IParkingBusiness policeParking;
        public PolicemanController(IParkingBusiness policeParking)
        {
            this.policeParking = policeParking;
        }

        /// <summary>
        /// Policeman Vehical Parking.
        /// </summary>
        /// <param name="park">The park.</param>
        /// <returns></returns>

        [HttpPost]
        [Route("policeVehicalPark")]
        public IActionResult PolicemanVehicalPark([FromBody] ParkingModel park)
        {
            try
            {
                var result = this.policeParking.ParkingVehical(park);
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

        /// <summary>
        /// Unpark Vehical if the entry is true.
        /// </summary>
        /// <param name="unpark">The unpark.</param>
        /// <returns></returns>

        [HttpPut]
        [Route("driverVehicalUnpark")]
        public IActionResult PolicemanVehicalUnpark(int id)
        {
            try
            {
                var unparks = this.policeParking.UnparkingVehical(id);

                if (unparks.IsEmpty == true)
                {
                    return this.Ok(new { success = true, Message = "Unpark", Data = unparks });
                }
                if (unparks.IsEmpty == false)
                {
                    return this.Ok(new { success = true, Message = "Park", Data = unparks });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, Message = "Error While Updating" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Delete the vehical if its False.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        
        [HttpDelete]
        [Route("deleteVehical")]
        public IActionResult DeleteVehical()
        {
            try
            {
                bool delete = this.policeParking.DeleteVehicals();

                if (delete)
                {
                    return this.Ok(new { success = true, Message = "Empty Vehicale Slots Deleted Success", Data = delete });
                }
                else
                {
                    return this.BadRequest(new { sucess = false, Message = "Error While Deleting" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = false, Message = e.Message });
            }
        }


    }
}
