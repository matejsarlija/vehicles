using Microsoft.EntityFrameworkCore;
using Vehicles.Data;
using Vehicles.Models;

namespace Vehicles.Service;

public class VehicleMakeRepository : IVehicleMakeRepository
{
    private readonly VehicleContext _context;

    public VehicleMakeRepository(VehicleContext context)
    {
        _context = context;
    }
    
    public async Task<List<VehicleMake>> GetVehicleMakesForModelsAsync()
    {
        return await _context.VehicleMake.ToListAsync();
    }
    
    public async Task<PaginatedList<VehicleMake>> GetVehicleMakesAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber)
    {
        if (searchString != null)
        {
            pageNumber = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        var vehicleMakes = _context.VehicleMake.AsQueryable();

        if (!String.IsNullOrEmpty(searchString))
        {
            vehicleMakes = vehicleMakes.Where(v => v.Name.Contains(searchString) ||
                                                   v.Abrv.Contains(searchString));
        }
        
        switch (sortOrder)
        {
            case "name_desc":
                vehicleMakes = vehicleMakes.OrderByDescending(v => v.Name);
                break;
            case "abrv":
                vehicleMakes = vehicleMakes.OrderBy(v => v.Abrv);
                break;
            case "abrv_desc":
                vehicleMakes = vehicleMakes.OrderByDescending(v => v.Abrv);
                break;
            default:
                vehicleMakes = vehicleMakes.OrderBy(v => v.Name);
                break;
        }

        int pageSize = 3;
        
        return await PaginatedList<VehicleMake>.CreateAsync(vehicleMakes, pageNumber ?? 1, pageSize);
    }

    public async Task<VehicleMake> GetVehicleMakeByIdAsync(int id)
    {
        var vehicleMake = await _context.VehicleMake.Include(v => v.VehicleModels)
            .FirstOrDefaultAsync(m => m.Id == id);
        return vehicleMake;
    }

    public async Task CreateVehicleMakeAsync(VehicleMake vehicleMake)
    {
        _context.VehicleMake.Add(vehicleMake);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateVehicleMakeAsync(VehicleMake vehicleMake)
    {
        _context.VehicleMake.Update(vehicleMake);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteVehicleMakeAsync(int id)
    {
        var vehicleMake = await _context.VehicleMake.FindAsync(id);
        if (vehicleMake != null)
        {
            _context.VehicleMake.Remove(vehicleMake);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> VehicleMakeExistsAsync(int id)
    {
        return await _context.VehicleMake.AnyAsync(v => v.Id == id);
    }
}