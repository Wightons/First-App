using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.API.Contracts;
using TaskBoard.API.Dtos;

namespace TaskBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase, IBaseCrudController<BoardDto>
    {

        private readonly IBaseEntityService<BoardDto> _boardsService;
        public BoardsController(IBaseEntityService<BoardDto> boardsService)
        {
            _boardsService = boardsService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BoardDto dto)
        {
            try
            {
                await _boardsService.Add(dto);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _boardsService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message});
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var res = await _boardsService.Get(id);
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var res = await _boardsService.GetAll();
                return Ok(res);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BoardDto dto)
        {
            try
            {
                await _boardsService.Update(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
