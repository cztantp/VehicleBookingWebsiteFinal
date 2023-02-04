using VehicleBookingWebsite.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleBookingWebsite.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<OrderVehicle> OrderVehicle { get; }
        IGenericRepository<Vehicle> Vehicles { get; }
        IGenericRepository<VehicleType> VehicleType { get; }
        IGenericRepository<Staff> Staff { get; }
        IGenericRepository<Customer> Customers { get; }
    }
}