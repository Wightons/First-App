using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TaskBoard.API.Contracts;
using TaskBoard.API.Database;
using TaskBoard.API.Database.Entities;
using TaskBoard.API.Dtos;

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
            var dto = new CardListDto() { Id = id };
            var mappedModel = _mapper.Map<CardList>(dto);

            try
            {
                _context.Lists.Attach(mappedModel);
                _context.Lists.Remove(mappedModel);
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

        public async Task Update(int id, CardListDto dto)
        {
            var model = await _context.Lists.FindAsync(id);
            if (model == null)
            {
                throw new Exception($"No list with id {id}");
            }

            var mappedDto = _mapper.Map<CardList>(dto);

            if (model.Name != mappedDto.Name)
            {
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
