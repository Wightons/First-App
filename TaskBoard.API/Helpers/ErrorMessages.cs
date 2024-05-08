namespace TaskBoard.API.Helpers
{
    public static class ErrorMessages
    {
        public static string CardWithIdNotFound(int id)
        {
            return $"Card with id {id} was not found";
        }

        public static string ConcurrentCardUpdateError()
        {
            return "Concurrent card update error";
        }
    }
}
