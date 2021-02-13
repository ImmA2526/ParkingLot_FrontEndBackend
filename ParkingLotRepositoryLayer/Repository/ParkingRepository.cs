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

        public ParkingModel UnparkingVehical(int id)
        {
            try
            {
                ParkingModel unPark = parkingContext.ParkingTable.Find(id);
                if (unPark.IsEmpty == false)
                {
                    unPark.IsEmpty = true;
                    parkingContext.Entry(unPark).State = EntityState.Modified;
                    parkingContext.SaveChangesAsync();
                    return unPark;
                }
                return null;
                //else
                //{
                //    unPark.IsEmpty = true;
                //    parkingContext.Entry(unPark).State = EntityState.Modified;
                //    parkingContext.SaveChangesAsync();
                //    return unPark;
                //}
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
                throw new Exception("Error While Deleting Empty Slots" + e.Message);
            }
        }

        /// <summary>
        /// Searches the vehical.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>

        public ParkingModel SearchVehicalByVehicalNo(ParkingModel search)
        {
            try
            {
                IEnumerable<ParkingModel> searchResult = parkingContext.ParkingTable.Where(e => e.VehicalNo == search.VehicalNo).ToList();
                if (searchResult != null)
                {
                    foreach (var searchvalue in searchResult)
                    {
                        this.parkingContext.ParkingTable.Find(searchResult);
                        return (ParkingModel)searchResult;
                    }
                    return (ParkingModel)searchResult;
                }
                return (ParkingModel)searchResult;
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
        public ParkingModel SearchVehicalBySLotNo(ParkingModel search)
        {
            try
            {
                IEnumerable<ParkingModel> searchResult = parkingContext.ParkingTable.Where(e => e.SlotNo == search.SlotNo).ToList();
                if (searchResult != null)
                {
                    foreach (var searchvalue in searchResult)
                    {
                        this.parkingContext.ParkingTable.Find(searchResult);
                        return (ParkingModel)searchResult;
                    }
                    return (ParkingModel)searchResult;
                }
                return (ParkingModel)searchResult;
            }
            catch (Exception e)
            {
                throw new Exception("Error While Searcing" + e.Message);
            }
        }

    }
}
