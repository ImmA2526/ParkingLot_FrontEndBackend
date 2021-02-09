using Microsoft.EntityFrameworkCore;
using ParkingLotModelLayer;
using System;
namespace ParkingLotRepositoryLayer
{
    public class ParkingContext : DbContext
    {
       public ParkingContext(DbContextOptions<ParkingContext> options) : base(options)
        {

        }

        public DbSet<UserModel> UserTable { get; set; }
    }
}
