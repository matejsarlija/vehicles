using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Vehicles.Models;

namespace Vehicles.Data;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new VehicleContext(
                   serviceProvider.GetRequiredService<DbContextOptions<VehicleContext>>()))
        {
            if (context.VehicleMake.Any())
            {
                return; // DB has been seeded already
                
            }
            
            context.VehicleMake.AddRange(
                new VehicleMake
                {
                    Name = "Bayerische Motoren Werke",
                    Abrv = "BMW"
                },
                new VehicleMake
                {
                    Name = "Fabbrica Italiana Automobili di Torino",
                    Abrv = "FIAT"
                },
                new VehicleMake
                {
                    Name = "Volkswagen",
                    Abrv = "VW"
                }
            );
            
            context.VehicleModel.AddRange(
                new VehicleModel
                {
                    Name = "Passat",
                    Abrv = "Passat",
                    VehicleMakeId = 3,
                },
                new VehicleModel
                {
                    Name = "335i",
                    Abrv = "3",
                    VehicleMakeId = 1,
                },
                new VehicleModel
                {
                    Name = "500",
                    Abrv = "500",
                    VehicleMakeId = 2,
                }
            );
            context.SaveChanges();
        }
    }
}