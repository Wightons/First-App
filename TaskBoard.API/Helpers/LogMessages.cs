namespace TaskBoard.API.Helpers
{
    public class LogMessages
    {
        //<b></b>
        public static string CardMovedToList(string cardName, string list1, string list2)
        {
            return $"Card <b>{cardName}</b> was moved from <b>{list1}</b> tp <b>{list2}</b>";
        }

        public static string CardDeletedFromList(string cardName, string list)
        {
            return $"Card <b>{cardName}</b> was deleted from <b>{list}</b>";
        }

        public static string CardAddedToList(string cardName, string list)
        {
            return $"Card <b>{cardName}</b> was added to <b>{list}</b>";
        }

        public static string CardUpdatedInList(string cardNameBefore, string list, string? cardNameAfter = null)
        {
            if (cardNameAfter!= null && cardNameBefore != cardNameAfter)
            {
                return $"Card <b>{cardNameBefore}</b> was updated to <b>{cardNameAfter}</b> in <b>{list}</b>";
            }
            return $"Card <b>{cardNameBefore}</b> was updated in <b>{list}</b>";
        }

        public static string ListDeleted(string list)
        {
            return $"List <b>{list}</b> was deleted";
        }

        public static string ListCreated(string list)
        {
            return $"List <b>{list}</b> was created";
        }

        public static string ListUpdated(string listBefore, string listAfter = null)
        {
            if (listAfter != null && listBefore != listAfter)
            {
                return $"List <b>{listBefore}</b> was updated to <b>{listAfter}</b>";
            }
            return $"Card <b>{listBefore}</b> was updated";
        }

    }
}
