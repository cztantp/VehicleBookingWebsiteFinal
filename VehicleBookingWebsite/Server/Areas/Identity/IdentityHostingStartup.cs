using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VehicleBookingWebsite.Server.Data;
using VehicleBookingWebsite.Server.Models;

[assembly: HostingStartup(typeof(VehicleBookingWebsite.Server.Areas.Identity.IdentityHostingStartup))]
namespace VehicleBookingWebsite.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}