using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TaskBoard.API.Contracts;
using TaskBoard.API.Database;
using TaskBoard.API.Database.Entities;
using TaskBoard.API.Dtos;
using TaskBoard.API.Helpers;

namespace TaskBoard.API.Services
{
    public class CardsService : IBaseEntityService<CardDto>
    {
        private readonly TaskBoardContext _context;
        private readonly IMapper _mapper;
        public CardsService(TaskBoardContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(CardDto dto)
        {
            if (!Enum.IsDefined(typeof(Priority), dto.Priority ?? 0))
            {
                throw new ArgumentException("No such code of priority");
            }

            try
            {
                var mappedModel = _mapper.Map<Card>(dto);
                await _context.Cards.AddAsync(mappedModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var exNumber = int.Parse(((PostgresException)ex.InnerException!).SqlState);
                if (exNumber == 23503)
                {
                    throw new Exception($"Cannot add new Card, no list with id {dto.ListId}");
                }
                if (exNumber == 23505)
                {
                    throw new Exception($"Card with id {dto.Id} already exists");
                }
                throw new Exception(ex.Message);
            }
        }

        public async Task Delete(int id)
        {
            var dto = new CardDto() { Id = id };
            var mappedModel = _mapper.Map<Card>(dto);

            try
            {
                _context.Cards.Attach(mappedModel);
                _context.Cards.Remove(mappedModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"No card with id {id}");
            }
        }

        public async Task<CardDto> Get(int id)
        {
            var model = await _context.Cards.FindAsync(id);
            if (model == null)
            {
                throw new KeyNotFoundException(ErrorMessages.CardWithIdNotFound(id));
            }

            var mappedModel = _mapper.Map<CardDto>(model);

            return mappedModel;
        }

        public async Task<IEnumerable<CardDto>> GetAll()
        {
            var modelsList = await _context.Cards.ToListAsync();
            var mappedList = _mapper.Map<IEnumerable<CardDto>>(modelsList);
            return mappedList;
        }

        public async Task Update(int id, CardDto dto)
        {
            var model = await _context.Cards.FindAsync(id);
            if (model == null)
            {
                throw new KeyNotFoundException(ErrorMessages.CardWithIdNotFound(id));
            }

            var modelId = model.Id;
            _mapper.Map(dto, model);
            model.Id = modelId;

            await _context.SaveChangesAsync();

        }
    }
}
