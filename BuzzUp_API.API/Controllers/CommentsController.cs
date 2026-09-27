using BuzzUp_API.Application.DTO.Comments;
using BuzzUp_API.Application.UseCases.Commands.Comments;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public CommentsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CommentUpdateDTO dto, [FromServices] IUpdateCommentCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
