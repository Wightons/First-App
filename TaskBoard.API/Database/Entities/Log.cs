namespace TaskBoard.API.Database.Entities
{
    public class Log : BaseModel
    {
        public string Message { get; set; }
        //relation props
        public int CardId { get; set; }
    }
}
