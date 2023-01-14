using Vehicles.Models;

namespace Vehicles.Service;

public interface IVehicleMakeRepository
{
    Task<PaginatedList<VehicleMake>> GetVehicleMakesAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber);
    Task<VehicleMake> GetVehicleMakeByIdAsync(int id);
    Task CreateVehicleMakeAsync(VehicleMake vehicleMake);
    Task UpdateVehicleMakeAsync(VehicleMake vehicleMake);
    Task DeleteVehicleMakeAsync(int id);
    Task<bool> VehicleMakeExistsAsync(int id);
}