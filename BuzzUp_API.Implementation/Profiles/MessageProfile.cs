using AutoMapper;
using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.Profiles
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<MessageInsertDTO, Message>()
                .ForMember(dest => dest.SenderId, opt => opt.Ignore())
                .ForMember(dest => dest.IsRead, opt => opt.Ignore());
        }
    }
}
