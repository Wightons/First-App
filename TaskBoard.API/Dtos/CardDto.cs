using TaskBoard.API.Database.Entities;

namespace TaskBoard.API.Dtos
{
    public class CardDto : BaseDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public int? Priority { get; set; }
        public int? ListId { get; set; }
    }
}
