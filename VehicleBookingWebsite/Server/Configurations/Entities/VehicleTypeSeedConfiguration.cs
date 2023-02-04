using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleBookingWebsite.Shared.Domain;

namespace VehicleBookingWebsite.Server.Configurations.Entities
{
    public class VehicleTypeSeedConfiguration : IEntityTypeConfiguration<VehicleType>
    {
        public void Configure(EntityTypeBuilder<VehicleType> builder)
        {
            builder.HasData(
                new VehicleType
                {
                    Id = 1,
                    Name = "Camry",
                    Description = "Comfortable and supportive seats. Has the safety equipment one can expect from a car of this class"
                },

                new VehicleType
                {
                    Id = 2,
                    Name = "G-CLASS G63 AMG 4MATIC",
                    Description = "Mileage: 15,500 km, can drive fast"
                }
                );
        }
    }
}
