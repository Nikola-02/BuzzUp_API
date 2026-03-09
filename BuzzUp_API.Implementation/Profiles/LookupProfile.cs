using AutoMapper;
using BuzzUp_API.Application.DTO;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.Profiles
{
    public class LookupProfile : Profile
    {
        public LookupProfile()
        {
            CreateMap<Role, LookupMiniDTO>();
        }
    }
}
