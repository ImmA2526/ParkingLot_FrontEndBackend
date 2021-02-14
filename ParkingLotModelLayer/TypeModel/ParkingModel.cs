using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string VehicalNo { get; set; }

        [Required]
        public int SlotNo { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool IsEmpty { get; set; }

        public DateTime EntryTime { get; set; }
        
        [DefaultValue("0000-00-00 00:00:00")]
        public DateTime ExitTime { get; set; }

        [Required]
        public int Charges { get; set; }

        [Required]
        [ForeignKey("ParkingTypeModel")]
        public int ParkTypeID { get; set; }
        
        [Required]
        [ForeignKey("VehicalTypeModel")]
        public int VehicleTypeID { get; set; }

        [Required]
        [ForeignKey("DriverTypeModel")]
        public int DriverTypeID { get; set; }
        
    }
}
