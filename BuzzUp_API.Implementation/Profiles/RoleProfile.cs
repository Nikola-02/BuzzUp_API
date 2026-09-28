using AutoMapper;
using BuzzUp_API.Application.DTO.Roles;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleInsertDTO, Role>();
            CreateMap<RoleUpdateDTO, Role>();
        }
    }
}
