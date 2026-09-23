using BuzzUp_API.Application.DTO.Friendships;
using BuzzUp_API.Application.UseCases.Commands.Friendships;
using BuzzUp_API.Application.UseCases.Queries.Friendships;
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

        [HttpGet]
        public IActionResult Get([FromQuery] FriendSearch search, [FromServices] IGetMyFriendsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search ?? new FriendSearch()));
        }

        [HttpGet("incoming")]
        public IActionResult GetIncoming([FromServices] IGetIncomingFriendRequestsQuery query)
        {
            return Ok(_handler.HandleQuery(query, new FriendSearch()));
        }

        [HttpPost]
        public IActionResult Post([FromBody] FriendshipInsertDTO dto, [FromServices] ISendFriendRequestCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPost("accept")]
        public IActionResult Accept([FromBody] FriendshipInsertDTO dto, [FromServices] IAcceptFriendRequestCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpPost("reject")]
        public IActionResult Reject([FromBody] FriendshipInsertDTO dto, [FromServices] IRejectFriendRequestCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
