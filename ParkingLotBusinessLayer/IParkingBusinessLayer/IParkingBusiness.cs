using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotBusinessLayer.IParkingBusinessLayer
{
    public interface IParkingBusiness
    {
        ParkingModel ParkingVehical(ParkingModel park);

        ParkingModel UnparkingVehical(ParkingModel unpark);

        ParkingModel SearchVehical(ParkingModel search);

        ParkingModel DeleteUnparkVehical(ParkingModel delete);
    }
}
