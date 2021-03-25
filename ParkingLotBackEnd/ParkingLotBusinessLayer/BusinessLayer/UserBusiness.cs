using ParkingLotBusinessLayer.IBusinessLayer;
using ParkingLotModelLayer;
using System;

using ParkingLotRepositoryLayer.IRepository;

namespace ParkingLotBusinessLayer
{
    public class UserBusiness : IUserBusiness
    {
        IUserRepository userRepo;
        public UserBusiness(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public UserModel UserRegistration(UserModel model)
        {
            var result = userRepo.UserRegistration(model);
            return result;
        }

        public UserModel UserLogin(LoginModel login)
        {
            var result = userRepo.UserLogin(login);
            return result;
        }

        public string ForgotUserPassword(string Email)
        {
            var result = userRepo.ForgotUserPassword(Email);
            return result;
        }

        public string ResetUserPassword(LoginModel reset)
        {
            var result = userRepo.ResetUserPassword(reset);
            return result;
        }

        public VehicalTypeModel VehicalTypes(VehicalTypeModel vehical)
        {
            var result = userRepo.VehicalTypes(vehical);
            return result;
        }

        public DriverTypeModel DriverTypes(DriverTypeModel driver)
        {
            var result = userRepo.DriverTypes(driver);
            return result;
        }

        public ParkingTypeModel ParkingTypes(ParkingTypeModel parking)
        {
            var result = userRepo.ParkingTypes(parking);
            return result;
        }
    }
}