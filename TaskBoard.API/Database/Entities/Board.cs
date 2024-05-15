namespace TaskBoard.API.Database.Entities
{
    public class Board : BaseModel
    {
        public string Name { get; set; }
        public ICollection<CardList> Lists { get; set; }
    }
}
