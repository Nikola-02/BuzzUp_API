using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.Application.UseCases.Commands.Country;
using BuzzUp_API.Application.UseCases.Queries.Country;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuzzUp_API.API.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private UseCaseHandler _handler;

        public CountryController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<CountryController>
        [HttpGet]
        public IActionResult Get([FromQuery] CountrySearch search, [FromServices] IGetCountriesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        [HttpPost]
        public IActionResult Post([FromBody] CountryInsertDTO dto, [FromServices] ICreateCountryCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CountryUpdateDTO dto, [FromServices] IUpdateCountryCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCountryCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
