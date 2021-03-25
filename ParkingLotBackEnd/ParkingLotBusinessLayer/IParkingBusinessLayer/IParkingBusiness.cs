using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotBusinessLayer.IParkingBusinessLayer
{
    public interface IParkingBusiness
    {
        ParkingModel ParkingVehical(ParkingModel park);

        ParkingResponse UnparkingVehical(int parkingId);

        bool DeleteVehicals();

        IEnumerable<ParkingModel> SearchVehical(int slotNo,string vehicalNo);

        IEnumerable<ParkingModel> GetParkVehicalData();
    }
}
