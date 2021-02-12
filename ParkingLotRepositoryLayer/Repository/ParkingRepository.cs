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
                ParkingModel unPark = parkingContext.ParkingTable.Find(unpark);
                if (unPark.IsEmpty == true)
                {
                    return unPark;
                }

                parkingContext.ParkingTable.Remove(unPark);
                parkingContext.SaveChangesAsync();
                return unpark;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Deleting" + e.Message);
            }
        }

    }
}
