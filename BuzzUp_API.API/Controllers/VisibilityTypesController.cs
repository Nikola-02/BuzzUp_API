using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.UseCases.Queries.Visibility;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/visibilityTypes")]
    [ApiController]
    public class VisibilityTypesController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public VisibilityTypesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] TablesSearch search, [FromServices] IGetVisibilityTypesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search ?? new TablesSearch()));
        }
    }
}
