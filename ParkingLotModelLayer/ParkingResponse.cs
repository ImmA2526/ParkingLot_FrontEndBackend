using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParkingLotModelLayer
{
    public class ParkingResponse
    {
        [Key]
        public int ParkingId { get; set; }

        public string VehicalNo { get; set; }

        public int SlotNo { get; set; }

        public bool IsEmpty { get; set; }

        public int EntryTime { get; set; }

        public DateTime ExitTime { get; set; }

        public double ParkingTypeCharges { get; set; }

        public double VehicalTypeCharges { get; set; }
        
        public double DriverTypeCharges { get; set; }

        public string ParkType { get; set; }

        public string VehicleType { get; set; }

        public string DriverType { get; set; }

        public double TotalCharges { get; set; }
    }
}
