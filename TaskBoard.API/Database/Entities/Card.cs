namespace TaskBoard.API.Database.Entities
{
    public enum Priority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
    public class Card : BaseModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateOnly DueDate { get; set; }
        public required Priority Priority { get; set; }
        //relation props
        public int ListId { get; set; }
        public required CardList List { get; set; }
    }
}
