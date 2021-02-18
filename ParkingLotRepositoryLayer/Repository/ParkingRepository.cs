using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IRepository;
using PMSMQ;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace ParkingLotRepositoryLayer.Repository
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly ParkingContext parkingContext;
        public ParkingRepository(ParkingContext parkingContext)
        {
            this.parkingContext = parkingContext;
        }

        /// <summary>
        /// Parking the Vehical.
        /// </summary>//
        /// <param name="IsEmpty">if set to <c>true</c> [is empty].</param>
        /// <returns></returns>

        public ParkingModel ParkingVehical(ParkingModel park)
        {
            try
            {
                if (park.DriverTypeID > 0 && park.DriverTypeID < 5)
                {
                    park.EntryTime = DateTime.Now;
                    park.ExitTime = new DateTime(0000 - 0000 - 000);
                    parkingContext.ParkingTable.Add(park);
                    var result = parkingContext.SaveChanges();
                    return park;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error While Parking" + e.Message);
            }
        }

        /// <summary>
        /// Unparkings the vehical.
        /// </summary>
        /// <param name="unpark">The unpark.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Adding" + e.Message</exception>

        public ParkingResponse UnparkingVehical(int parkingId)
        {
            try
            {
                var parkingResult = this.parkingContext.ParkingTable.Where<ParkingModel>(model => model.ParkingId == parkingId).SingleOrDefault();
                if (parkingResult != null)
                {
                    //Unpark Vehical if the Vehical is Alredy Parked
                    if (parkingResult.IsEmpty)
                    {
                        parkingResult.IsEmpty = false;
                        var result = CalculateCharge(parkingResult.ParkingId);
                        var exitTime = DateTime.Now;
                        parkingResult.ExitTime = exitTime;
                        parkingResult.Charges = (int)(result.VehicalTypeCharges + result.DriverTypeCharges + result.ParkingTypeCharges);
                        result.TotalCharges = parkingResult.Charges;
                        this.parkingContext.ParkingTable.Update(parkingResult);
                        this.parkingContext.SaveChangesAsync();
                        return result;
                    }

                    //Park Vehical if the Vehical is Not Park
                    else
                    {
                        string subject = "Your Parking Detail is";
                        var result = CalculateCharge(parkingResult.ParkingId);
                        parkingResult.IsEmpty = true;
                        parkingResult.Charges = 0;
                        parkingResult.EntryTime = DateTime.Now;
                        parkingResult.ExitTime = new DateTime(0000 - 0000 - 000);
                        this.parkingContext.ParkingTable.Update(parkingResult);
                        this.parkingContext.SaveChangesAsync();

                        var userId = parkingResult.userId;
                        var results = parkingContext.UserTable.FirstOrDefault(e => e.userId == userId);
                        var email = results.Email;
                        if (results != null)
                        {
                            ///Sending Mail Regarding with Parking Data...

                            var parkVehical = "<h1>Parking Detail </h1><div><b>Hi " + results.FirstName + " </b>,<br></div>" +
                           "<table border=1px;><tr><td>Vehical No</td><td>" + parkingResult.VehicalNo + "</td></tr>"
                           + "<tr><td>Password</td><td><b>" + parkingResult.EntryTime + "</b></td></tr></table>";

                            Sender send = new Sender();
                            send.MailSender(parkVehical);

                            Recever recev = new Recever();
                            var Parking = recev.MailReciver();
                            var body = Parking;
                            using (MailMessage mailMessage = new MailMessage("imraninfo.1996@gmail.com", email))
                            {
                                mailMessage.Subject = subject;
                                mailMessage.Body = body;
                                mailMessage.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp.gmail.com";
                                smtp.EnableSsl = true;
                                NetworkCredential NetworkCred = new NetworkCredential("imraninfo.1996@gmail.com", "9175833272");
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = 587;
                                smtp.Send(mailMessage);
                            }

                            return result;
                        }
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Unparking" + e.Message);
            }
        }

        /// <summary>
        /// Calculating Charges For Unpark
        /// </summary>
        /// <param name="parkingId"></param>
        /// <returns></returns>
        public ParkingResponse CalculateCharge(int parkingId)
        {
            var result = from parkingModel in parkingContext.ParkingTable
                         join parkingTypeModel in parkingContext.ParkingTypeTable
                         on parkingModel.ParkingTypeID equals parkingTypeModel.ParkingTypeID

                         join driverTypeModel in parkingContext.DriverTypeTable
                         on parkingModel.DriverTypeID equals driverTypeModel.DriverTypeID

                         join vehicalTypeModel in parkingContext.VehicalTypeTable
                         on parkingModel.VehicleTypeID equals vehicalTypeModel.VehicleTypeID

                         select new ParkingResponse()
                         {
                             ParkingId = parkingModel.ParkingId,
                             VehicalNo = parkingModel.VehicalNo,
                             SlotNo = parkingModel.SlotNo,
                             IsEmpty = parkingModel.IsEmpty,
                             EntryTime = parkingModel.EntryTime,
                             ExitTime = parkingModel.ExitTime,
                             ParkingTypeCharges = parkingTypeModel.Charges,
                             VehicalTypeCharges = vehicalTypeModel.Charges,
                             DriverTypeCharges = driverTypeModel.Charges,
                             ParkingType = parkingTypeModel.ParkingType,
                             DriverType = driverTypeModel.DriverType,
                             VehicleType = vehicalTypeModel.VehicalType,
                             TotalCharges = 0,
                         };
            foreach (var data in result)
            {
                if (data.ParkingId == parkingId)
                {
                    return data;
                }
            }
            return null;
        }
        /// <summary>
        /// Deletes the vehical if the slot is Empty.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Deleting Empty Slots" + e.Message</exception>

        public bool DeleteVehicals()
        {
            try
            {
                IEnumerable<ParkingModel> delete = parkingContext.ParkingTable.Where(x => x.IsEmpty == false).ToList();
                if (delete != null)
                {
                    foreach (var unpark in delete)
                    {
                        parkingContext.ParkingTable.Remove(unpark);
                        parkingContext.SaveChangesAsync();
                    }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Deleting Data" + e.Message);
            }
        }

        /// <summary>
        /// Searches the vehical by slot no OR By Vehical No.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Searcing" + e.Message</exception>

        public IEnumerable<ParkingModel> SearchVehical(int slotNo, string vehicalNo)
        {
            try
            {
                IEnumerable<ParkingModel> searchResult = parkingContext.ParkingTable.Where(e => e.SlotNo == slotNo || e.VehicalNo == vehicalNo).ToList();
                if (searchResult != null)
                {
                    return searchResult;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Searcing" + e.Message);
            }
        }


        /// <summary>
        /// Get all ParkVehical Data 
        /// </summary>
        /// <param name="parkingID"></param>
        /// <returns></returns>

        public IEnumerable<ParkingModel> GetParkVehicalData()
        {
            try
            {
                IEnumerable<ParkingModel> getResult = parkingContext.ParkingTable.Where(e => e.IsEmpty == true).ToList();
                if (getResult != null)
                {
                    return getResult;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Retriving Data " + e.Message);
            }
        }
    }
}
