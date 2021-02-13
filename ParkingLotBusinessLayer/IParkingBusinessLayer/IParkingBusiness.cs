using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotBusinessLayer.IParkingBusinessLayer
{
    public interface IParkingBusiness
    {
        ParkingModel ParkingVehical(ParkingModel park);

        ParkingModel UnparkingVehical(int id);

        bool DeleteVehicals();

        ParkingModel SearchVehicalBySLotNo(ParkingModel search);
        ParkingModel SearchVehicalByVehicalNo(ParkingModel search);
    }
}
