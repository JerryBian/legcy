using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Chat.ClientWpf
{
    public class ChatClient
    {
        private readonly string _userId;
        private readonly PlayerInfo _playerInfo;

        public ChatPeerListener MasterServerListener { get; private set; }

        public ChatPeerListener GameServerListener { get; private set; }


        public ChatClient()
        {
            _userId = Guid.NewGuid().ToString("N");
            Trace.TraceInformation($"User:: {_userId}");
            _playerInfo = new PlayerInfo { UserId = _userId, LobbyName = "London" };
        }

        public void StartGameServerPeer(string serverAddress, string applicationName)
        {
            GameServerListener = new ChatPeerListener(this, _playerInfo, serverAddress, applicationName);
        }

        public void Execute()
        {
            // 1. connect to master server
            MasterServerListener = new ChatPeerListener(this, _playerInfo, "127.0.0.1:5055", "MasterServer");

            // 2. send auth to master server
            MasterServerListener.SendOperation((byte)OpCodes.Auth, new Dictionary<byte, object> { { 225, _userId } });

            // 3. list lobby status
            // _masterServerListener.SendOperation((byte)OpCodes.LobbyStats, new Dictionary<byte, object> { });

            // 4. join lobby
            // _masterServerListener.SendOperation((byte)OpCodes.JoinLobby, new Dictionary<byte, object> { { (byte)213, "Paris" }, { 212, (byte)0 } });

            // 4. find matcher player
            // _masterServerListener.SendOperation((byte)OpCodes.FindMatcherPlayer, new Dictionary<byte, object>());

            // 5. create game
            // _masterServerListener.SendOperation((byte)OpCodes.CreateGame, new Dictionary<byte, object>());

            // 6. send messages
            // _masterServerListener.SendOperation((byte)OpCodes.SendMessage, new Dictionary<byte, object>());


        }
    }
}
