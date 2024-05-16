namespace TaskBoard.API.Database.Entities
{
    public class CardList : BaseModel
    {
        public required string Name { get; set; }
        //relation props
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
