﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskBoard.API.Contracts;
using TaskBoard.API.Dtos;

namespace TaskBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase, IBaseCrudController<CardListDto>
    {
        private readonly IBaseEntityService<CardListDto> _listsService;
        public ListsController(IBaseEntityService<CardListDto> listsService)
        {
            _listsService = listsService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var cardDto = await _listsService.Get(id);
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
            var res = await _listsService.GetAll();
            if (res.Count() == 0)
            {
                return NoContent();
            }
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CardListDto dto)
        {
            try
            {
                await _listsService.Add(dto);
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
                await _listsService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CardListDto dto)
        {
            try
            {
                await _listsService.Update(id, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
