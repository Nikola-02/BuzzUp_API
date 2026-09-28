using BuzzUp_API.Application.DTO.Chats;
using BuzzUp_API.Application.UseCases.Queries.Chats;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ChatsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetMyChatsQuery query)
        {
            return Ok(_handler.HandleQuery(query, new ChatSearch()));
        }

        [HttpGet("{id}/messages")]
        public IActionResult GetMessages(int id, [FromServices] IGetChatMessagesQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        [HttpPost]
        public IActionResult Post([FromBody] ChatOpenDTO dto, [FromServices] IOpenDirectChatQuery query)
        {
            return Ok(_handler.HandleQuery(query, dto));
        }
    }
}
