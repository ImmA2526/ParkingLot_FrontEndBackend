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

        public ParkingModel UnparkingVehical(ParkingModel unPark)
        {
            var result = parkingRepo.ParkingVehical(unPark);
            return result;
        }

        public ParkingModel SearchVehical(ParkingModel search)
        {
            var searchResult = parkingRepo.SearchVehical(search);
            return searchResult;
        }

        public ParkingModel DeleteUnparkVehical(ParkingModel unpark)
        {
            var deleteResult = parkingRepo.DeleteUnparkVehical(unpark);
            return deleteResult;
        }

    }
}
