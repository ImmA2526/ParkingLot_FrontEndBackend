using ParkingLotModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingLotBusinessLayer.IBusinessLayer
{
    public interface IUserBusiness
    {
        UserModel UserRegistration(UserModel model);

        LoginModel UserLogin(LoginModel login);

        string ForgotUserPassword(ForgotModel forgot);

        string ResetUserPassword(string oldPassword, string newPassword);
    }
}
