using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleBookingWebsite.Shared.Domain;

namespace VehicleBookingWebsite.Server.Configurations.Entities
{
    public class VehicleSeedConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.HasData(
               new Vehicle
               {
                   Id = 1,
                   LicensePlateNumber = "SMC1234C",
                   Colour = "Black",
                   Year = DateTime.Now.Year,
                   Brand = "Toyota",
                   PassengerCapacity = 4,
                   Price = 18.90
               },
               new Vehicle
               {
                   Id = 2,
                   LicensePlateNumber = "SMC5678C",
                   Colour = "Red",
                   Year = DateTime.Now.Year,
                   Brand = "MMERCEDES-BENZ",
                   PassengerCapacity = 4,
                   Price = 20.40
               }
            );
        }

    }
}
