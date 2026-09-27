using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.Application.UseCases;
using System.Collections.Generic;

namespace BuzzUp_API.Application.UseCases.Queries.Comments
{
    public interface IGetCommentsQuery : IQuery<List<CommentDTO>, int>
    {
    }
}
