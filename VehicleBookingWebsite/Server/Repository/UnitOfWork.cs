using VehicleBookingWebsite.Server.Data;
using VehicleBookingWebsite.Server.IRepository;
using VehicleBookingWebsite.Server.Models;
using VehicleBookingWebsite.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VehicleBookingWebsite.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Staff> _staffs;
        private IGenericRepository<Customer> _customers;
        private IGenericRepository<Order> _orders;
        private IGenericRepository<OrderVehicle> _ordervehicles;
        private IGenericRepository<Vehicle> _vehicles;
        private IGenericRepository<VehicleType> _vehicletypes;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Staff> Staff
            => _staffs ??= new GenericRepository<Staff>(_context);
        public IGenericRepository<Customer> Customers
            => _customers ??= new GenericRepository<Customer>(_context);
        public IGenericRepository<Order> Orders
            => _orders ??= new GenericRepository<Order>(_context);
        public IGenericRepository<OrderVehicle> OrderVehicle
            => _ordervehicles ??= new GenericRepository<OrderVehicle>(_context);
        public IGenericRepository<Vehicle> Vehicles
            => _vehicles ??= new GenericRepository<Vehicle>(_context);
        public IGenericRepository<VehicleType> VehicleType
            => _vehicletypes ??= new GenericRepository<VehicleType>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
        
        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            await _context.SaveChangesAsync();
        }
    }
}