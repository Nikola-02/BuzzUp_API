using BuzzUp_API.Application.DTO;
using BuzzUp_API.Application.DTO.Roles;
using BuzzUp_API.Application.UseCases.Commands.Roles;
using BuzzUp_API.Application.UseCases.Queries.Roles;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public RolesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] TablesSearch search, [FromServices] IGetRolesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpPost]
        public IActionResult Post([FromBody] RoleInsertDTO dto, [FromServices] ICreateRoleCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RoleUpdateDTO dto, [FromServices] IUpdateRoleCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRoleCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
