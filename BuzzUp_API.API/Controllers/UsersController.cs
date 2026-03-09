using BuzzUp_API.Application.DTO.Users;
using BuzzUp_API.Application.UseCases.Commands.Account;
using BuzzUp_API.Application.UseCases.Commands.Users;
using BuzzUp_API.Application.UseCases.Queries.Users;
using BuzzUp_API.Implementation;
using BuzzUp_API.Implementation.UseCases.Queries.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UsersController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] UserSearch search, [FromServices] IGetUsersQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSingleUserQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // POST api/<AuthController>
        [HttpPost("register")]
        public IActionResult Register([FromBody] UserInsertDTO userDto, [FromServices] IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, userDto);
            return StatusCode(201);
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword([FromBody] string email, [FromServices] IForgotPasswordUserCommand command, [FromServices] AppSettings appSettings)
        {
            ForgotPasswordDto dto = new ForgotPasswordDto();
            dto.UserEmail = email;
            dto.SmtpEmail = appSettings.Email.SmtpUser;
            dto.SmtpPassword = appSettings.Email.SmtpPass;
            dto.ResetPasswordUrl = appSettings.Frontend.ResetPasswordUrl;
            _handler.HandleCommand(command, dto);
            return Ok();
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto,[FromServices] IResetPasswordUserCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] UserUpdateDTO dto, [FromServices] IUpdateUserCommand command)
        {
            dto.Id = id;
            _handler.HandleCommand(command, dto);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
