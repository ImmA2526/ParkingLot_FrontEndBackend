//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using ParkingLotBusinessLayer.IBusinessLayer;
//using ParkingLotModelLayer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ParkingLotApplication.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
    
//    public class DriverController : ControllerBase
//    {

//        public IUserBusiness business;
//        public DriverController(IUserBusiness business)
//        {
//            this.business = business;
//        }

//        [HttpPost]
//        [Route("driverparking")]
//        public IActionResult DriverParking([FromBody] DriverTypeModel park)
//        {
//            try
//            {
//                var result = this.business.DriverVehicalParking(park);
//                if (result != null)
//                {
//                    return this.Ok(new { success = true, Message = "Data Added Succesfully", Data = result });
//                }
//                else
//                {
//                    return this.BadRequest(new { sucess = false, Message = "There is not Empty Slot" });
//                }
//            }
//            catch (Exception e)
//            {
//                return this.NotFound(new { status = false, Message = e.Message });
//            }
//        }

//    }
//}
