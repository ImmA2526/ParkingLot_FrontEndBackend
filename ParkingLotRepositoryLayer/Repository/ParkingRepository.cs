using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ParkingLotModelLayer;
using ParkingLotRepositoryLayer.IRepository;
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
        /// </summary>
        /// <param name="IsEmpty">if set to <c>true</c> [is empty].</param>
        /// <returns></returns>

        public ParkingModel ParkingVehical(ParkingModel park)
        {
            try
            {
                parkingContext.ParkingTable.Add(park);
                var result = parkingContext.SaveChanges();
                if (result > 0)
                {
                    return park;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Adding" + e.Message);
            }
        }

        /// <summary>
        /// Unparkings the vehical.
        /// </summary>
        /// <param name="unpark">The unpark.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Adding" + e.Message</exception>

        public ParkingResponse UnparkingVehical(int slotNo)
        {
            try
            {
                //ParkingModel unPark = parkingContext.ParkingTable.Find(slotNo);
                var parkingResult = this.parkingContext.ParkingTable.Where<ParkingModel>(model => model.SlotNo==slotNo && model.IsEmpty==false).FirstOrDefault();
                var result = CalculateCharge(parkingResult.ParkingId);
                var exitTime = DateTime.Now;
                parkingResult.ExitTime = exitTime;
                parkingResult.IsEmpty = true;
                parkingResult.Charges = (int)(result.VehicalTypeCharges + result.DriverTypeCharges + result.ParkingTypeCharges);
                result.TotalCharges = parkingResult.Charges;
                this.parkingContext.ParkingTable.Update(parkingResult);
                this.parkingContext.SaveChangesAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Deleting" + e.Message);
            }
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
                throw new Exception("Error While Deleting" + e.Message);
            }
        }

        /// <summary>
        /// Searches the vehical.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>

        public IEnumerable<ParkingModel> SearchVehicalByVehicalNo(string vehicalNo)
        {
            try
            {
                IEnumerable<ParkingModel> searchResult = parkingContext.ParkingTable.Where(e => e.VehicalNo == vehicalNo).ToList();
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
        /// Searches the vehical by s lot no.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Error While Searcing" + e.Message</exception>
        public IEnumerable<ParkingModel> SearchVehicalBySLotNo(int slotNo)
        {
            try
            {
                IEnumerable<ParkingModel> searchResult = parkingContext.ParkingTable.Where(e => e.SlotNo == slotNo).ToList();
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
        /// Calculating Charges For Unpark
        /// </summary>
        /// <param name="parkingId"></param>
        /// <returns></returns>
        public ParkingResponse CalculateCharge(int parkingId)
        {
            var result = from parkingModel in parkingContext.ParkingTable
                         join parkingTypeModel in parkingContext.ParkingTypeTable
                         on parkingModel.ParkTypeID equals parkingTypeModel.ParkTypeID
                         join driverTypeModel in parkingContext.DriverTypeTable
                         on parkingModel.ParkTypeID equals driverTypeModel.DriverTypeID
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
                             ParkType = parkingTypeModel.ParkType,
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
    }
}
