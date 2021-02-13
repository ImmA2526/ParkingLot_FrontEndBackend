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
        [Route("driverVehicalUnpark")]
        public IActionResult PolicemanVehicalUnpark(int id)
        {
            try
            {
                var unparks = this.policeParking.UnparkingVehical(id);

                if (unparks.IsEmpty == true)
                {
                    return this.Ok(new { Status = true, Message = "Unpark", Data = unparks });
                }
                if (unparks.IsEmpty == false)
                {
                    return this.Ok(new { Status = true, Message = "Park", Data = unparks });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error While Updating" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
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
                    return this.Ok(new { Status = true, Message = "Empty Vehicale Slots Deleted Status", Data = delete });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Error While Deleting" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        /// <summary>
        /// Searches the vehical by slotNo an Vehical No.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>

        [HttpGet]
        [Route("searchVehicalBySlotNo")]
        public IActionResult SearchVehicalBySlot(ParkingModel search)
        {
            try
            {
                IEnumerable<ParkingModel> searchResult = this.policeParking.SearchVehicalBySLotNo(search);
                return this.Ok(searchResult);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("searchVehicalByVehicalNo")]
        public IActionResult SearchVehicalByVehical(ParkingModel search)
        {
            try
            {
                IEnumerable<ParkingModel> searchResult = this.policeParking.SearchVehicalByVehicalNo(search);
                return this.Ok(searchResult);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
