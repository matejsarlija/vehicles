using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vehicles.Models;

namespace Vehicles.Data
{
    public class VehicleMakeContext : DbContext
    {
        public VehicleMakeContext (DbContextOptions<VehicleMakeContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicles.Models.VehicleMake> VehicleMake { get; set; } = default!;
    }
}
