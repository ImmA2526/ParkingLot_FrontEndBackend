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

        VehicalTypeModel VehicalTypes(VehicalTypeModel vehical);

        DriverTypeModel DriverTypes(DriverTypeModel driver);
        
        ParkingTypeModel ParkingTypes(ParkingTypeModel parking);
    }
}
