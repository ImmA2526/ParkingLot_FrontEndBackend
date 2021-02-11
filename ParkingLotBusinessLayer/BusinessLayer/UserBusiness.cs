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

        public UserModel UserLogin(LoginModel login)
        {
            var result = repository.UserLogin(login);
            return result;
        }

        public string ForgotUserPassword(ForgotModel forgot)
        {
            var result = repository.ForgotUserPassword(forgot);
            return result;
        }

        public string ResetUserPassword(LoginModel reset)
        {
            var result = repository.ResetUserPassword(reset);
            return result;
        }

        public ParkingModel ParkingVehical(ParkingModel park)
        {
            var result = repository.ParkingVehical(park);
            return result;
        }

        public DriverTypeModel OwnerVehicalParking(DriverTypeModel park)
        {
            var result = repository.OwnerVehicalParking(park);
            return result;
        }

        public VehicalTypeModel VehicalTypes(VehicalTypeModel vehical)
        {
            var result = repository.VehicalTypes(vehical);
            return result;
        }
    }
}
