using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotBusinessLayer.IBusinessLayer
{
    public interface IUserBusiness
    {
        UserModel UserRegistration(UserModel model);

        UserModel UserLogin(LoginModel login);

        string ForgotUserPassword(ForgotModel forgot);

        string ResetUserPassword(LoginModel reset);

        ParkingModel ParkingVehical(ParkingModel park);

        DriverTypeModel OwnerVehicalParking(DriverTypeModel park);

        VehicalTypeModel VehicalTypes(VehicalTypeModel vehical);

        //DriverTypeModel PolicemanParking(DriverTypeModel park);
        //DriverTypeModel DriverVehicalParking(DriverTypeModel park);

        //DriverTypeModel SecurityParking(DriverTypeModel park);
    }
}
