using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotModelLayer;
using System;

using ParkingLotRepositoryLayer.IRepository;
using ParkingLotBusinessLayer.IParkingBusinessLayer;

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

        public ParkingModel UnparkingVehical(int id)
        {
            var result = parkingRepo.UnparkingVehical(id);
            return result;
        }

        public bool DeleteVehicals()
        {
            var delete = parkingRepo.DeleteVehicals();
            return delete;
        }

        public ParkingModel SearchVehicalByVehicalNo(ParkingModel search)
        {
            var searchResult = parkingRepo.SearchVehicalByVehicalNo(search);
            return searchResult;
        }

        public ParkingModel SearchVehicalBySLotNo(ParkingModel search)
        {
            var searchResult = parkingRepo.SearchVehicalBySLotNo(search);
            return searchResult;
        }
    }
}
