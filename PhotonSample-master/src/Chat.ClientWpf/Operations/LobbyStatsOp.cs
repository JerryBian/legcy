using System.Collections.Generic;
using ExitGames.Client.Photon;

namespace Chat.ClientWpf.Operations
{
    public class LobbyStatsOp : IBaseOp
    {
        public byte OpCode => (byte)OpCodes.LobbyStats;

        public void OnResponse(ChatPeerListener peerListener, OperationResponse response)
        {
            peerListener.SendOperation((byte)OpCodes.JoinLobby, new Dictionary<byte, object> { { 213, peerListener.Player.LobbyName }, { 212, (byte)0 } });
        }
    }
}
