using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotModelLayer;
using System;

using ParkingLotRepositoryLayer.IRepository;

namespace ParkingLotBusinessLayer
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository repository;
        public UserBusiness(IUserRepository repository)
        {
            this.repository = repository;
        }

        public UserModel UserRegistration(UserModel model)
        {
            var result = repository.UserRegistration(model);
            return result;
        }

        public LoginModel UserLogin(LoginModel login)
        {
            var result = repository.UserLogin(login);
            return result;
        }

        public string ForgotUserPassword(ForgotModel forgot)
        {
            var result = repository.ForgotUserPassword(forgot);
            return result;
        }

        public string ResetUserPassword(string oldPassword, string newPassword)
        {
            var result = repository.ResetUserPassword(oldPassword, newPassword);
            return result;
        }

    }
}
