namespace Vehicles.Models;

public class VehicleMakeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abrv { get; set; }
    
    public IEnumerable<VehicleModel> VehicleModels { get; set; }

}