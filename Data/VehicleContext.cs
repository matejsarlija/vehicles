using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicles.Models;

namespace Vehicles.Data
{
    public class VehicleContext : DbContext
    {
        public VehicleContext (DbContextOptions<VehicleContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicles.Models.VehicleMake> VehicleMake { get; set; } = default!;

        public DbSet<Vehicles.Models.VehicleModel> VehicleModel { get; set; } = default!;
    }
}
