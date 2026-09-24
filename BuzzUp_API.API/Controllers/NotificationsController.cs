using BuzzUp_API.Application.DTO.Notifications;
using BuzzUp_API.Application.UseCases.Commands.Notifications;
using BuzzUp_API.Application.UseCases.Queries.Notifications;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public NotificationsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromServices] IGetNotificationsQuery query)
        {
            return Ok(_handler.HandleQuery(query, new NotificationSearch()));
        }

        [HttpPost("read")]
        public IActionResult MarkAllRead([FromServices] IMarkAllNotificationsReadCommand command)
        {
            _handler.HandleCommand(command, new NotificationSearch());
            return NoContent();
        }

        [HttpPost("{id}/read")]
        public IActionResult MarkRead(int id, [FromServices] IMarkNotificationReadCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
