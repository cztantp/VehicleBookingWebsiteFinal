using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBookingWebsite.Shared.Domain
{
    public class Staff: BaseDomainModel
    {
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First Name does not meet length requirements")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last Name does not meet length requirements")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(6|8|9)\d{7}", ErrorMessage = "Contact Number is not a valid phone number")]
        public string Contact { get; set; }
        public string Address { get; set; }

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [RegularExpression(@"^[STFGstfg]\d{7}[A-Za-z]", ErrorMessage = "Driving License does not meet NRIC requirements")] //NRIC Regular Expression
        public string DrivingLicenseNumber { get; set; }
        public string Password { get; set; }
    }
}
