using AutoMapper;
using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.Application.UseCases.Queries.Posts;
using BuzzUp_API.DataAccess;
using BuzzUp_API.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Implementation.UseCases.Queries.Posts
{
    public class EfGetSinglePostQuery : EfFindUseCase<PostDTO, Post>, IGetSinglePostQuery
    {
        public EfGetSinglePostQuery(BuzzUpContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public override int Id => 12;

        public override string Name => "Find Post";
    }
}
