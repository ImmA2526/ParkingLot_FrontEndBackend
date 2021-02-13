using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotBusinessLayer.IParkingBusinessLayer
{
    public interface IParkingBusiness
    {
        ParkingModel ParkingVehical(ParkingModel park);

        ParkingResponse UnparkingVehical(int slotNo);

        bool DeleteVehicals();

        IEnumerable<ParkingModel> SearchVehicalByVehicalNo(string vehicalNo);

        IEnumerable<ParkingModel> SearchVehicalBySLotNo(int slotNo);
    }
}
