using AutoMapper;
using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Profiles
{
    public class ReactionTypeProfile : Profile
    {
        public ReactionTypeProfile()
        {
            CreateMap<ReactionType, ReactionTypeDTO>();
        }
    }
}
