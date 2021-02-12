//using microsoft.aspnetcore.http;
//using microsoft.aspnetcore.mvc;
//using parkinglotbusinesslayer.ibusinesslayer;
//using parkinglotmodellayer;
//using system;
//using system.collections.generic;
//using system.linq;
//using system.threading.tasks;

//namespace parkinglotapplication.controllers
//{
//    [route("api/[controller]")]
//    [apicontroller]

//    public class drivercontroller : controllerbase
//    {

//        public iuserbusiness business;
//        public drivercontroller(iuserbusiness business)
//        {
//            this.business = business;
//        }

//        [httppost]
//        [route("driverparking")]
//        public iactionresult driverparking([frombody] drivertypemodel park)
//        {
//            try
//            {
//                var result = this.business.drivervehicalparking(park);
//                if (result != null)
//                {
//                    return this.ok(new { success = true, message = "data added succesfully", data = result });
//                }
//                else
//                {
//                    return this.badrequest(new { sucess = false, message = "there is not empty slot" });
//                }
//            }
//            catch (exception e)
//            {
//                return this.notfound(new { status = false, message = e.message });
//            }
//        }

//    }
//}
