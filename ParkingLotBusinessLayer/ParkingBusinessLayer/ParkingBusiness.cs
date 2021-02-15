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
        public ParkingBusiness(IParkingRepository parkingRepo)
        {
            this.parkingRepo = parkingRepo;
        }


        public ParkingModel ParkingVehical(ParkingModel park)
        {
            var result = parkingRepo.ParkingVehical(park);
            return result;
        }

        public ParkingResponse UnparkingVehical(int parkingId)
        {
            var result = parkingRepo.UnparkingVehical(parkingId);
            return result;
        }

        public bool DeleteVehicals()
        {
            var delete = parkingRepo.DeleteVehicals();
            return delete;
        }

        public IEnumerable<ParkingModel> SearchVehical(string vehicalNo)
        {
            var searchResult = parkingRepo.SearchVehical(vehicalNo);
            return searchResult;
        }

        public IEnumerable<ParkingModel> SearchVehical(int slotNo)
        {
            var searchResult = parkingRepo.SearchVehical(slotNo);
            return searchResult;
        }

        public IEnumerable<ParkingModel> GetParkVehicalData()
        {
            var getResult = parkingRepo.GetParkVehicalData();
            return getResult;
        }
    }
}
