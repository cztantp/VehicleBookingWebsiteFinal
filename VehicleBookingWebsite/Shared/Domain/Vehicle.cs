using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBookingWebsite.Shared.Domain
{
    public class Vehicle: BaseDomainModel
    {
        [Required]
        [RegularExpression(@"^[A-Zaz]{3}\d{4}[A-Za-z]", ErrorMessage = "License Plate Number does not meet requirements")]
        public string LicensePlateNumber { get; set; }

        [Required]
        public string Colour { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public int PassengerCapacity { get; set; }

        [Required]
        public Double Price { get; set; }
        public virtual List<Order> Orders { get; set; }

        public int? StaffID { get; set; }
        public virtual Staff Staff { get; set; }

        public int? VehicleTypeID { get; set; }
        public virtual VehicleType VehicleType { get; set; }
    }
}
