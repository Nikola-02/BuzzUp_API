using BuzzUp_API.Application.DTO.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuzzUp_API.Application.UseCases.Queries.Posts
{
    public interface IGetSinglePostQuery : IQuery<PostDTO, int>
    {
    }
}
