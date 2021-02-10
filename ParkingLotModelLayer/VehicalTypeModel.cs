using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingLotModelLayer
{
    public class VehicalTypeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int VehicleTypeID { get; set; }

        public string VehicalType { get; set; }

        public int Charges { get; set; }

        public int VehicalNo {get;set;}
    }
}
