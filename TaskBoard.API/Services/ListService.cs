using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;
using TaskBoard.API.Contracts;
using TaskBoard.API.Database;
using TaskBoard.API.Database.Entities;
using TaskBoard.API.Dtos;
using TaskBoard.API.Helpers;

namespace TaskBoard.API.Services
{
    public class ListService : IBaseEntityService<CardListDto>
    {
        private readonly TaskBoardContext _context;
        private readonly IMapper _mapper;
        public ListService(TaskBoardContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(CardListDto dto)
        {
            try
            {
                var mappedModel = _mapper.Map<CardList>(dto);
                await _context.AddAsync(mappedModel);

                var logToAdd = new Log { Message = LogMessages.ListCreated(mappedModel.Name) };
                await _context.Logs.AddAsync(logToAdd);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var exNumber = int.Parse(((PostgresException)ex.InnerException!).SqlState);
                if (exNumber == 23505)
                {
                    throw new Exception($"List with id {dto.Id} already exists");
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var cd = _context.Lists.Find(id);
                _context.Cards.RemoveRange(_context.Cards.Where(c => c.ListId == id).ToList());

                _context.Lists.Attach(cd);
                _context.Lists.Remove(cd);

                var logToAdd = new Log { Message = LogMessages.ListDeleted(cd.Name) };
                await _context.Logs.AddAsync(logToAdd);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"No list with id {id}");
            }
        }

        public async Task<CardListDto> Get(int id)
        {
            var model = await _context.Lists.FindAsync(id);
            if (model == null)
            {
                throw new KeyNotFoundException($"No list with id {id}");
            }

            var mappedModel = _mapper.Map<CardListDto>(model);

            return mappedModel;
        }

        public async Task<IEnumerable<CardListDto>> GetAll()
        {
            var modelsList = await _context.Lists.ToListAsync();
            var mappedList = _mapper.Map<IEnumerable<CardListDto>>(modelsList);
            return mappedList;
        }

        public async Task<IEnumerable<CardListDto>> GetByBoardId(int boardId)
        {
            var modelsList = await _context.Lists.Where(l => l.BoardId == boardId).ToListAsync();
            var mappedList = _mapper.Map<IEnumerable<CardListDto>>(modelsList);
            return mappedList;
        }

        public async Task Update(int id, CardListDto dto)
        {
            var model = await _context.Lists.FindAsync(id);
            var nameBefore = model.Name;
            if (model == null)
            {
                throw new Exception($"No list with id {id}");
            }

            var mappedDto = _mapper.Map<CardList>(dto);

            if (model.Name != mappedDto.Name)
            {
                var logToAdd = new Log { Message = LogMessages.ListUpdated(nameBefore, mappedDto.Name) };
                await _context.Logs.AddAsync(logToAdd);

                model.Name = mappedDto.Name;
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    throw new Exception("Save error");
                }
            }

        }
    }
}
