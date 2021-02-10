using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingLotModelLayer
{
    public class ParkingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ParkingId { get; set; }

        [Required]
        public int VehicalNo { get; set; }

        [Required]
        public int SlotNo { get; set; }

        [Required]
        public bool IsEmpty { get; set; }

        [Required]
        public int EntryTime { get; set; }

        [Required]
        public int ExitTime { get; set; }

        [Required]
        public int Charges { get; set; }

        [Required]
        public int ParkTypeID { get; set; }
        
        [ForeignKey("ParkTypeID")]
        public ParkingTypeModel ParkingTypeModel { get; set; }

        [Required]
        public int VehicleTypeID { get; set; }

        [ForeignKey("VehicleTypeID")]
        public VehicalTypeModel VehicalTypeModel { get; set; }

        [Required]
        public int DriverTypeID { get; set; }
        
        [ForeignKey("DriverTypeID")]
        public DriverTypeModel DriverTypeModel { get; set; }
    }
}
