using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public FriendshipsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] FriendshipInsertDTO dto, [FromServices] ISendFriendRequestCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
    }
}
