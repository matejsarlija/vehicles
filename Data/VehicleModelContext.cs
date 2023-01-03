using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicles.Models;

namespace Vehicles.Data
{
    public class VehicleModelContext : DbContext
    {
        public VehicleModelContext (DbContextOptions<VehicleModelContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicles.Models.VehicleModel> VehicleModel { get; set; } = default!;
    }
}
