using System.Collections.Generic;
using System.Diagnostics;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class CreateGameOp : IBaseOp
    {
        public byte OpCode => (byte)OpCodes.CreateGame;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            Trace.TraceInformation("========================CREATED GAME(MASTER)===========================");
            peerListener.ChatClient.StartGameServerPeer($"{response.Parameters[230]}", "GameServer");
            peerListener.ChatClient.GameServerListener.SendOperation(100, new Dictionary<byte, object> { {255, response.Parameters[255]}, {239, true} });
        }
    }
}
