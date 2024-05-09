using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskBoard.API.Database;
using TaskBoard.API.Database.Entities;
using TaskBoard.API.Dtos;

namespace TaskBoard.API.Services
{
    public class LogService
    {
        private readonly TaskBoardContext _context;
        private readonly IMapper _mapper;
        public LogService(TaskBoardContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LogDto>> GetMessages(int? cardId = null)
        {
            var modelsLog = new List<Log>();
            if (cardId.HasValue)
            {
                modelsLog = await _context.Logs.Where(l => l.CardId == cardId).ToListAsync();
            }
            else
            {
                modelsLog = await _context.Logs.ToListAsync();
            }
            var mappedLog = _mapper.Map<IEnumerable<LogDto>>(modelsLog);
            return mappedLog;
        }

    }
}
