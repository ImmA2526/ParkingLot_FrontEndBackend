using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotModelLayer;
using System;

using ParkingLotRepositoryLayer.IRepository;
using ParkingLotBusinessLayer.IParkingBusinessLayer;
using System.Collections.Generic;

namespace ParkingLotBusinessLayer
{
    public class ParkingBusiness : IParkingBusiness
    {
        IParkingRepository parkingRepo;
        public ParkingBusiness (IParkingRepository parkingRepo)
        {
            this.parkingRepo = parkingRepo;
        }

       
        public ParkingModel ParkingVehical(ParkingModel park)
        {
            var result = parkingRepo.ParkingVehical(park);
            return result;
        }

        public ParkingResponse UnparkingVehical(int id)
        {
            var result = parkingRepo.UnparkingVehical(id);
            return result;
        }

        public bool DeleteVehicals()
        {
            var delete = parkingRepo.DeleteVehicals();
            return delete;
        }

        public IEnumerable<ParkingModel> SearchVehicalByVehicalNo(string vehicalNo)
        {
            var searchResult = parkingRepo.SearchVehicalByVehicalNo(vehicalNo);
            return (IEnumerable<ParkingModel>)searchResult;
        }

        public IEnumerable<ParkingModel> SearchVehicalBySLotNo(int slotNo)
        {
            var searchResult = parkingRepo.SearchVehicalBySLotNo(slotNo);
            return (IEnumerable<ParkingModel>)searchResult;
        }
    }
}
