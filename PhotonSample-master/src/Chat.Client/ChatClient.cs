using System;
using System.Collections.Generic;
using System.Threading;

namespace Chat.Client
{
    public class ChatClient
    {
        private readonly MasterClient _masterClient;
        private readonly string _userId;

        public ChatClient()
        {
            _userId = Guid.NewGuid().ToString();
            _masterClient = new MasterClient();
            _masterClient.Connect("127.0.0.1:5055", "MasterServer");
            var t = new Timer(state => { _masterClient.Service(); }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
        }

        public void Execute()
        {
            Auth();
            JoinLobby();
            LobbyStatus();
            CreateGame();
            FindFriend();

        }

        public void Auth()
        {
            _masterClient.SendOperation((byte)OpCodes.Auth, new Dictionary<byte, object> { { 225, _userId } });
        }

        public void JoinLobby()
        {
            _masterClient.SendOperation((byte)OpCodes.JoinLobby, new Dictionary<byte, object> { { (byte)213, "Paris" }, { 212, (byte)0 } });
        }

        public void LeaveLobby()
        {
            _masterClient.SendOperation((byte)OpCodes.LeaveLobby, new Dictionary<byte, object> { });
        }

        public void CreateGame()
        {
            _masterClient.SendOperation((byte)OpCodes.CreateGame, new Dictionary<byte, object> { {255, "GameId"} });
        }

        public void FindFriend()
        {
            _masterClient.SendOperation((byte)OpCodes.FindFriend, new Dictionary<byte, object> { { 1, new[] { _userId } } });
        }

        public void LobbyStatus()
        {
            _masterClient.SendOperation((byte)OpCodes.LobbyStats, new Dictionary<byte, object> { });
        }
    }
}
