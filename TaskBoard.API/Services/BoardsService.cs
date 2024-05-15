using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskBoard.API.Contracts;
using TaskBoard.API.Database;
using TaskBoard.API.Database.Entities;
using TaskBoard.API.Dtos;

namespace TaskBoard.API.Services
{
    public class BoardsService : IBaseEntityService<BoardDto>
    {
        private readonly TaskBoardContext _context;
        private readonly IMapper _mapper;
        public BoardsService(TaskBoardContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(BoardDto model)
        {
            var mappedModel = _mapper.Map<Board>(model);
            await _context.Boards.AddAsync(mappedModel);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            try
            {
                var model = await _context.Boards.FindAsync(id);
                _context.Boards.Remove(model!);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<BoardDto> Get(int id)
        {
            try
            {
                var model = await _context.Boards.FindAsync(id);
                var dto = _mapper.Map<BoardDto>(model);
                return dto;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<BoardDto>> GetAll()
        {
            var modelsList = await _context.Boards.ToListAsync();
            var mappedList = _mapper.Map<IEnumerable<BoardDto>>(modelsList);
            return mappedList;
        }

        public async Task Update(int id, BoardDto model)
        {
            try
            {
                var boardById = await _context.Boards.FindAsync(id);
                if (boardById == null)
                {
                    throw new KeyNotFoundException("board not found");
                }

                if (boardById.Name != model.Name)
                {
                    boardById.Name = model.Name;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
