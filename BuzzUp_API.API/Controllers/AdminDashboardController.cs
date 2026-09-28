using BuzzUp_API.Application.DTO.Admin;
using BuzzUp_API.Application.UseCases.Queries.Admin;
using BuzzUp_API.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace BuzzUp_API.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        private readonly UseCaseHandler _handler;

        public AdminDashboardController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("stats")]
        public IActionResult GetStats([FromServices] IGetAdminDashboardStatsQuery query)
        {
            return Ok(_handler.HandleQuery(query, new AdminDashboardStatsSearch()));
        }

        [HttpGet("logs")]
        public IActionResult GetLogs([FromQuery] UseCaseLogSearch search, [FromServices] IGetUseCaseLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search ?? new UseCaseLogSearch()));
        }
    }
}
