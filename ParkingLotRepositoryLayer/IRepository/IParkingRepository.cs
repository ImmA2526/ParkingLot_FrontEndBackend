using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotRepositoryLayer.IRepository
{
    public interface IParkingRepository
    {
        ParkingModel ParkingVehical(ParkingModel park);

        ParkingResponse UnparkingVehical(int parkingId);

        bool DeleteVehicals();

        IEnumerable<ParkingModel> SearchVehical(string vehicalNo);

        IEnumerable<ParkingModel> SearchVehical(int slotNo);

        IEnumerable<ParkingModel> GetParkVehicalData(bool IsEmpty);

        //IEnumerable<ParkingModel> GetParkVehicalData(int parkingID);
    }
}
