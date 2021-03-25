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

        string ForgotUserPassword(string Email);

        string ResetUserPassword(LoginModel reset);

        VehicalTypeModel VehicalTypes(VehicalTypeModel vehical);

        DriverTypeModel DriverTypes(DriverTypeModel driver);  
        
        ParkingTypeModel ParkingTypes(ParkingTypeModel parking);
    }
}
