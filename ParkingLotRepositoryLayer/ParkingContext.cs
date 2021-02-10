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
        public DbSet<ParkingModel> ParkingTable { get; set; }
        public DbSet<DriverTypeModel> DriverTable { get; set; }
        public DbSet<VehicalTypeModel> VehicalTable { get; set; }
        public DbSet<ParkingTypeModel> ParkingTypeTable { get; set; }

    }
}
