using BuzzUp_API.Application.DTO.Country;
using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Queries.Country;
using BuzzUp_API.Application.UseCases.Queries.Users;
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

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CountryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
