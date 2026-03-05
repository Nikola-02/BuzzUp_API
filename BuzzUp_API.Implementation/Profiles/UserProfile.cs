using AutoMapper;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>();

            CreateMap<UserInsertDTO, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.Password, opt =>
                {
                    opt.PreCondition(src => !string.IsNullOrWhiteSpace(src.Password));
                    opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password));
                });
        }
    }
}
