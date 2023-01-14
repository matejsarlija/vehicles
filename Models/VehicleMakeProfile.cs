using AutoMapper;

namespace Vehicles.Models;

public class VehicleMakeProfile : Profile
{
    public VehicleMakeProfile()
    {
        CreateMap<VehicleMake, VehicleMakeViewModel>();
    }
}