using AutoMapper;
using LeadsAPI.DTOs;
using LeadsAPI.Entidades;

namespace LeadsAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<LeadDTO, Lead>();
            CreateMap<ContactDTO, Contact>();
            CreateMap<VehicleDTO, Vehicle>();
        }
    }
}
