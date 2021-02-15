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
    //[Authorize(Roles = "Policeman,Security")]
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
                if (result != null && park.DriverTypeID==2)
                {
                    return this.Ok(new { Status = true, Message = "Data Added Succesfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "There is not Empty Slot" });
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
        [Route("securityVehicalUnpark")]
        public IActionResult SecurityVehicalUnpark(int parkingId)
        {
            try
            {
                var unparks = this.securityParking.UnparkingVehical(parkingId);

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
                    return this.BadRequest(new { Status = false, Message = "Error While Updating" });
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
        [Route("searchVehical")]
        public IActionResult SearchVehical(int slotNo,string vehicalNo)
        {
            try
            {
                if (slotNo >0)
                {
                    IEnumerable<ParkingModel> searchResult = this.securityParking.SearchVehical(slotNo);
                    return this.Ok(searchResult);
                }
                else if (vehicalNo != null)
                {
                    IEnumerable<ParkingModel> searchResult = this.securityParking.SearchVehical(vehicalNo);
                    return this.Ok(searchResult);
                }
                return null;
            }

            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Gets all park vehical.
        /// </summary>
        /// <param name="parkingID">The parking identifier.</param>
        /// <returns></returns>

        [HttpGet]
        [Route("getparkVehical")]
        public IActionResult GetAllParkVehical(int parkingID)
        {
            try
            {
                if (parkingID > 0)
                {
                    IEnumerable<ParkingModel> getResult = this.securityParking.GetParkVehicalData(parkingID);
                    return this.Ok(getResult);
                }
                //else if (vehicalNo != null)
                //{
                //    IEnumerable<ParkingModel> searchResult = this.securityParking.SearchVehical(vehicalNo);
                //    return this.Ok(searchResult);
                //}
                return null;
            }

            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}
