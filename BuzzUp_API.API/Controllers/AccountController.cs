using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Commands.Account;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private UseCaseHandler _handler;

        public AccountController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<AccountController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserInsertUpdateDTO userDto, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, userDto);
            return StatusCode(201);
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
