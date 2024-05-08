using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBoard.API.Contracts;
using TaskBoard.API.Dtos;
using TaskBoard.API.Services;

namespace TaskBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase, IBaseCrudController<CardDto>
    {
        private readonly IBaseEntityService<CardDto> _cardsService;
        public CardsController(IBaseEntityService<CardDto> cardsService)
        {
            _cardsService = cardsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cardDto = await _cardsService.Get(id);
                return Ok(cardDto);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var res = await _cardsService.GetAll();
            if (res.Count() == 0)
            {
                return NoContent();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CardDto dto)
        {
            try
            {
                await _cardsService.Add(dto);
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
                await _cardsService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CardDto dto)
        {
            try
            {
                await _cardsService.Update(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
