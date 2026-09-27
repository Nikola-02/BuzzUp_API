using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.Application.UseCases;

namespace BuzzUp_API.Application.UseCases.Commands.Comments
{
    public interface ICreateCommentCommand : ICommand<CommentInsertDTO>
    {
    }
}
