using Microsoft.AspNetCore.Mvc.Rendering;

namespace Vehicles.Models;

public class VehicleModelViewModel
{
    public PaginatedList<VehicleModel> VehicleModels { get; set; }
    public SelectList VehicleMakes { get; set; }
    public string VehicleModelMake { get; set; }
    public string SearchString { get; set; }
}