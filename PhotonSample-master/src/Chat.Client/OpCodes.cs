namespace Chat.Client
{
    public enum OpCodes : byte
    {
        Auth = 230,

        CreateGame = 227,

        JoinLobby = 229,

        LeaveLobby = 228,

        FindFriend = 222,

        JoinChat = 226,

        LobbyStats = 221,

        SendMessage = 1,

        LeaveChat = 2
    }
}
