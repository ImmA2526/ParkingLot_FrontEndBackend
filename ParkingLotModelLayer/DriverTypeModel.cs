using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingLotModelLayer
{
    public class DriverTypeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DriverTypeID { get; set; }

        public string DriverType { get; set; }

        public int Charges { get; set; }
    }
}
