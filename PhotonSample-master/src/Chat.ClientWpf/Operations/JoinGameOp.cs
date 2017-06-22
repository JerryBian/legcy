using System;
using System.Collections.Generic;
using System.Diagnostics;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class JoinGameOp : IBaseOp
    {
        public byte OpCode => (byte)OpCodes.JoinChat;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            Trace.TraceInformation("======================JOIN GAME(MASTER)==============");
            peerListener.ChatClient.StartGameServerPeer($"{response.Parameters[230]}", "GameServer");
            peerListener.ChatClient.GameServerListener.SendOperation(101, new Dictionary<byte, object> { { 255, peerListener.Player.GameId }, { 239, true } });
        }
    }
}
