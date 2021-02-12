//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
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
//    public class PolicemanController : ControllerBase
//    {

//        private readonly IParking business;
//        //private readonly IConfiguration configuration;

//        public PolicemanController(IParking business, IConfiguration configuration)
//        {
//            this.business = business;
//            //this.configuration = configuration;
//        }

//        [HttpDelete]
//        [Route("deleteRecord")]
//        public IActionResult RemoveParkingRecord(bool IsEmpty)
//        {
//            try
//            {
//                bool result = this.business.DeleteRecord(IsEmpty);
//                if (IsEmpty==false)
//                {
//                    return this.Ok(result);
//                }
//                else
//                {
//                    return this.BadRequest();
//                }
//            }
//            catch (Exception e)
//            {
//                throw new Exception("Error While Deleting  Data" + e.Message);
//            }
//        }

//    }
//}
