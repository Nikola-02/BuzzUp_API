using BuzzUp_API.Application.DTO.Reactions;
using BuzzUp_API.Application.UseCases.Queries.Reactions;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactionTypesController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public ReactionTypesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ReactionTypeSearch search, [FromServices] IGetReactionTypesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search ?? new ReactionTypeSearch()));
        }
    }
}
