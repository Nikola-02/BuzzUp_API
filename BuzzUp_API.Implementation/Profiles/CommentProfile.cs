using AutoMapper;
using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.Domain;

namespace BuzzUp_API.Implementation.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentInsertDTO, Comment>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.LikeCount, opt => opt.Ignore())
                .ForMember(dest => dest.DislikeCount, opt => opt.Ignore());

            CreateMap<CommentUpdateDTO, Comment>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.PostId, opt => opt.Ignore())
                .ForMember(dest => dest.ParentId, opt => opt.Ignore())
                .ForMember(dest => dest.LikeCount, opt => opt.Ignore())
                .ForMember(dest => dest.DislikeCount, opt => opt.Ignore());

            CreateMap<Comment, CommentDTO>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.User.Image))
                .ForMember(dest => dest.Replies, opt => opt.Ignore());
        }
    }
}
