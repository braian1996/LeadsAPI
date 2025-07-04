using AutoMapper;
using LeadsAPI.DTOs;
using LeadsAPI.Entidades;

namespace LeadsAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Lead, LeadDTO>();
            CreateMap<LeadDTO, Lead>();

            CreateMap<Contact, ContactDTO>();
            CreateMap<ContactDTO, Contact>();

            CreateMap<Vehicle, VehicleDTO>();
            CreateMap<VehicleDTO, Vehicle>();
        }
    }
}
