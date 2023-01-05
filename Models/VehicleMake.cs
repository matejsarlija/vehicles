namespace Vehicles.Models;

public class VehicleMake
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Abrv { get; set; }

    public ICollection<VehicleModel> VehicleModels { get; set; }
}
