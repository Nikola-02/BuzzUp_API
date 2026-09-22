using BuzzUp_API.Application.DTO.Posts;
using BuzzUp_API.Application.UseCases.Commands.Posts;
using BuzzUp_API.Application.UseCases.Queries.Posts;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public PostsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        // GET: api/<PostsController>
        [HttpGet]
        public IActionResult Get([FromQuery] PostSearch search, [FromServices] IGetPostsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetSinglePostQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // POST api/<PostsController>
        [HttpPost]
        public IActionResult Post([FromBody] PostInsertDTO dto, [FromServices] ICreatePostCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
    }
}
