using AutoMapper;
using BuzzUp_API.Application.DTO.Feelings;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.Profiles
{
    public class FeelingTypeProfile : Profile
    {
        public FeelingTypeProfile()
        {
            CreateMap<FeelingType, FeelingTypeDTO>();
            CreateMap<FeelingTypeInsertDTO, FeelingType>();
            CreateMap<FeelingTypeUpdateDTO, FeelingType>();
        }
    }
}
