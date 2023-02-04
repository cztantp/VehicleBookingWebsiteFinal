using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleBookingWebsite.Shared.Domain
{
    public class Order : BaseDomainModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDateTime { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [Required]
        public int? StaffID { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
