using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotRepositoryLayer.IRepository
{
    public interface IParkingRepository
    {
        ParkingModel ParkingVehical(ParkingModel park);

        ParkingModel UnparkingVehical(int id);

        bool DeleteVehicals();

    }
}
