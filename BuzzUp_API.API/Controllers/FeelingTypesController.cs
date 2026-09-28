using BuzzUp_API.Application.DTO.Feelings;
using BuzzUp_API.Application.UseCases.Commands.Feelings;
using BuzzUp_API.Application.UseCases.Queries.Feelings;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/feelingTypes")]
    [ApiController]
    public class FeelingTypesController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public FeelingTypesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] FeelingTypeSearch search, [FromServices] IGetFeelingTypesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search ?? new FeelingTypeSearch()));
        }

        [HttpPost]
        public IActionResult Post([FromBody] FeelingTypeInsertDTO dto, [FromServices] ICreateFeelingTypeCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FeelingTypeUpdateDTO dto, [FromServices] IUpdateFeelingTypeCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteFeelingTypeCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
