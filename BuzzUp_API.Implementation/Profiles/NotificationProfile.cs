using AutoMapper;
using BuzzUp_API.Application.DTO.Notifications;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationDTO>()
                .ForMember(dest => dest.ActorId, opt => opt.MapFrom(src => src.Actor.Id))
                .ForMember(dest => dest.ActorFirstName, opt => opt.MapFrom(src => src.Actor.FirstName))
                .ForMember(dest => dest.ActorLastName, opt => opt.MapFrom(src => src.Actor.LastName))
                .ForMember(dest => dest.ActorUsername, opt => opt.MapFrom(src => src.Actor.Username))
                .ForMember(dest => dest.ActorImage, opt => opt.MapFrom(src => src.Actor.Image))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.NotificationType.Name))
                .ForMember(dest => dest.ReactionTypeName, opt => opt.MapFrom(src => src.ReactionType != null ? src.ReactionType.Name : null))
                .ForMember(dest => dest.ReactionTypeIcon, opt => opt.MapFrom(src => src.ReactionType != null ? src.ReactionType.Icon : null));
        }
    }
}
