using Vehicles.Models;

namespace Vehicles.Service;

public interface IVehicleMakeRepository
{
    Task<PaginatedList<VehicleMakeViewModel>> GetVehicleMakesAsync(string sortOrder, string currentFilter, string searchString, int? pageNumber);
    Task<VehicleMakeViewModel> GetVehicleMakeByIdAsync(int id);
    Task CreateVehicleMakeAsync(VehicleMakeViewModel vehicleMake);
    Task UpdateVehicleMakeAsync(VehicleMakeViewModel vehicleMake);
    Task DeleteVehicleMakeAsync(int id);
    Task<bool> VehicleMakeExistsAsync(int id);
}