using AutoMapper;
using BuzzUp_API.Application.DTO.Admin;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.Profiles
{
    public class UseCaseLogProfile : Profile
    {
        public UseCaseLogProfile()
        {
            CreateMap<UseCaseLog, UseCaseLogDTO>();
        }
    }
}
