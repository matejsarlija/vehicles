using AutoMapper;

namespace Vehicles.Models;

public class VehicleMakeProfile : Profile
{
    public VehicleMakeProfile()
    {
        CreateMap<VehicleMake, VehicleMakeViewModel>();
        CreateMap<VehicleMake, VehicleMakeViewModel>()
            .ForMember(dest => dest.VehicleModels, opt => opt.MapFrom(src => src.VehicleModels));
    }
}