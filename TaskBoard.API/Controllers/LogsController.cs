using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskBoard.API.Dtos;
using TaskBoard.API.Services;

namespace TaskBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogService _logService;

        public LogsController(LogService logService)
        {
            _logService = logService;
        }

        [HttpGet("{id?}")]
        public async Task<IActionResult> Get(int? id)
        {
            IEnumerable<LogDto> res;

            if (!id.HasValue)
            {
                res = await _logService.GetMessages();
            }
            else
            {
                res = await _logService.GetMessages(id);
            }

            if (res.Count() == 0)
            {
                return NoContent();
            }
            return Ok(res);
        }
    }
}
