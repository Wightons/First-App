namespace TaskBoard.API.Dtos
{
    public class CardListDto : BaseDto
    {
        public string? Name { get; set; }
        public int BoardId { get; set; }

    }
}
