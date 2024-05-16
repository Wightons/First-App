using Microsoft.EntityFrameworkCore;
using TaskBoard.API.Database.Entities;

namespace TaskBoard.API.Database
{
    public class TaskBoardContext : DbContext
    {
        public TaskBoardContext(DbContextOptions<TaskBoardContext> options)
        : base(options)
        {
        }

        public DbSet<Card> Cards { get; set; }
        public DbSet<CardList> Lists { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Board> Boards { get; set; }

    }
}
