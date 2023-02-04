using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBookingWebsite.Shared.Domain
{
    public class OrderVehicle: BaseDomainModel
    {
        [Required]
        public int? OrderID { get; set; }
        public virtual Order Order { get; set; }

        [Required]
        public int? VehicleID { get; set; }
        public virtual Vehicle Vehicle { get; set; }

        [Required]
        public int? VehicleTypeID { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}
