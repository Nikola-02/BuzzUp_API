using AutoMapper;
using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<PostInsertDTO, Post>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PostMedias, opt => opt.Ignore());

            CreateMap<PostUpdateDTO, Post>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PostMedias, opt => opt.Ignore());

            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.User.Image))
                .ForMember(dest => dest.VisibilityName, opt => opt.MapFrom(src => src.VisibilityType.Name))
                .ForMember(dest => dest.FeelingName, opt => opt.MapFrom(src => src.FeelingType != null ? src.FeelingType.Name : null))
                .ForMember(dest => dest.FeelingIcon, opt => opt.MapFrom(src => src.FeelingType != null ? src.FeelingType.Icon : null))
                .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.PostMedias
                    .Where(m => m.IsActive && m.DeletedAt == null)
                    .Select(m => m.Path)));
        }
    }
}
