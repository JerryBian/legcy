namespace Chat.ClientWpf
{
    public enum OpCodes : byte
    {
        Auth = 230,

        CreateGame = 227,

        CreateGame2 = 100,

        JoinLobby = 229,

        LeaveLobby = 228,

        FindFriend = 222,

        JoinChat = 226,

        JoinChat2 = 101,

        LobbyStats = 221,

        SendMessage = 1,

        LeaveChat = 2,

        FindMatcherPlayer = 3
    }
}
