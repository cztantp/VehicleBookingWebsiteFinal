using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleBookingWebsite.Client.Static
{
    public static class Endpoint
    {
        private static readonly string Prefix = "api";

        public static readonly string CustomersEndpoint = $"{Prefix}/customers";
        public static readonly string StaffsEndpoint = $"{Prefix}/staff";
        public static readonly string OrdersEndpoint = $"{Prefix}/orders";
        public static readonly string OrderVehiclesEndpoint = $"{Prefix}/ordervehicle";
        public static readonly string VehiclesEndpoint = $"{Prefix}/vehicles";
        public static readonly string VehicleTypesEndpoint = $"{Prefix}/vehicletype";
    }
}
