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
        public ParkingModel UnparkingVehical(ParkingModel unpark)
        {
            try
            {
                parkingContext.ParkingTable.Add(unpark);
                var result = parkingContext.SaveChanges();
                if (result > 0)
                {
                    return unpark;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Adding" + e.Message);
            }
        }
        
        /// <summary>
        /// Searches the vehical.
        /// </summary>
        /// <param name="vehicalNo">The vehical no.</param>
        /// <param name="slotNo">The slot no.</param>
        /// <returns></returns>
        public ParkingModel SearchVehical(ParkingModel search)
        {
            var searchResult = parkingContext.ParkingTable.Where(e => e.VehicalNo == search.VehicalNo && e.SlotNo == search.SlotNo);
            this.parkingContext.ParkingTable.Find(searchResult);
            return (ParkingModel)searchResult;
        }

        /// <summary>
        /// Deletes the unpark vehical.
        /// </summary>
        /// <param name="delete">The delete.</param>
        /// <returns></returns>
        public ParkingModel DeleteUnparkVehical(ParkingModel delete)
        {
            ParkingModel remove = parkingContext.ParkingTable.Find(delete.IsEmpty);
            if (remove == null)
            {
                if (remove.IsEmpty==false)
                {
                    return remove;
                }
            }

            parkingContext.ParkingTable.Remove(remove);
            parkingContext.SaveChangesAsync();
            return remove;
        }

    }
}
