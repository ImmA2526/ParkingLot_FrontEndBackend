using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotRepositoryLayer.IRepository
{
    public interface IUserRepository
    {
        UserModel UserRegistration(UserModel model);

        UserModel UserLogin(LoginModel login);

        string ForgotUserPassword(ForgotModel forgot);

        string ResetUserPassword(LoginModel reset);

        ParkingModel ParkingVehical(ParkingModel park);

        DriverTypeModel OwnerParkingVehical(DriverTypeModel park);

        //DriverTypeModel DriverParking(DriverTypeModel park);

        //DriverTypeModel PolicemanParking(DriverTypeModel park);

        //DriverTypeModel SecurityParking(DriverTypeModel park);
    }
}
